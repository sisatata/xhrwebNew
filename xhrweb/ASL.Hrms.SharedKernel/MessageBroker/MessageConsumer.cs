using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel.MessageBroker
{
    public class MessageConsumer : IMessageConsumer
    {

        private readonly IConfiguration _configuration;
        private IModel _channel;
        private IConnection _connection;
        private EventingBasicConsumer _basicConsumer;

        public MessageConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public bool Connect()
        {
            return Connect(_configuration["RabbitMqConnectionString"]);
        }

        public bool Connect(string rabbitMqConnectionString)
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(rabbitMqConnectionString),
                VirtualHost = "/",
                //Protocol = Protocols.DefaultProtocol,
                ContinuationTimeout = new TimeSpan(0, 5, 2),
                AutomaticRecoveryEnabled = true

                // user name, password, port will specify here
            };

            _connection = connectionFactory.CreateConnection();

            _connection.ConnectionShutdown += (object sender, ShutdownEventArgs e) =>
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs
                {
                    Cause = e.Cause,
                    ClassId = e.ClassId,
                    Initiator = e.Initiator.ToString(),
                    MethodId = e.MethodId,
                    ReplyCode = e.ReplyCode,
                    ReplyText = e.ReplyText
                });
            };

            return _connection.IsOpen;
        }

        public void Disconnect()
        {
            Dispose();
        }

        public void Subscribe(ConsumerOptions options)
        {
            _channel = _connection.CreateModel();

            _channel.ModelShutdown += (object sender, ShutdownEventArgs e) =>
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs
                {
                    Cause = e.Cause,
                    ClassId = e.ClassId,
                    Initiator = e.Initiator.ToString(),
                    MethodId = e.MethodId,
                    ReplyCode = e.ReplyCode,
                    ReplyText = e.ReplyText
                });
            };

            _channel.QueueDeclare(options.QueueName, true, false, false, null);
            _channel.QueueDeclare($"{options.QueueName}-LONG-RUNNING", true, false, false, null);

            _basicConsumer = new EventingBasicConsumer(_channel);

            if (options.PrefetchCount > 0)
            {
                _channel.BasicQos(prefetchSize: 0, prefetchCount: options.PrefetchCount, global: false);
                _channel.BasicConsume(options.QueueName, false, _basicConsumer);
                _channel.BasicConsume($"{options.QueueName}-LONG-RUNNING", false, _basicConsumer);
            }
            else
            {
                _channel.BasicConsume(options.QueueName, true, _basicConsumer);
                _channel.BasicConsume($"{options.QueueName}-LONG-RUNNING", true, _basicConsumer);
            }

            _basicConsumer.Received += async (object sender, BasicDeliverEventArgs @event) =>
            {
                var handled = false;
                //var message = Encoding.UTF8.GetString(@event.Body);

                try
                {
                    handled = await options.MessageReceivedEventHandler(@event.Body, @event.BasicProperties.Headers);
                }
                catch
                {
                    // write log here.
                }

                if (
                !handled
                && @event.BasicProperties != null
                && @event.BasicProperties.Headers != null
                && @event.BasicProperties.Headers.ContainsKey(MessageBusConstant.NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME))
                {
                    var numberOfRetryAttemptsLeft = (int)@event.BasicProperties.Headers[MessageBusConstant.NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME];

                    if (numberOfRetryAttemptsLeft > 0)
                    {
                        var requiresDelay = @event.BasicProperties.Headers.ContainsKey(MessageBusConstant.DELAY_HEADER_NAME);

                        @event.BasicProperties.Headers[MessageBusConstant.NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME] = numberOfRetryAttemptsLeft - 1;

                        if (requiresDelay)
                        {
                            var retryExchangeName = string.Format(MessageBusConstant.DELAYED_RETRY_EXCHANGE_NAME_FORMAT, options.QueueName);

                            var delayedRetryProperties = CreateDelayedRetryProperties(@event.BasicProperties.Headers[MessageBusConstant.DELAY_HEADER_NAME], numberOfRetryAttemptsLeft);

                            _channel.BasicPublish(exchange: retryExchangeName, routingKey: string.Empty, basicProperties: delayedRetryProperties, body: @event.Body);
                        }
                        else
                        {
                            _channel.BasicPublish(exchange: string.Empty, routingKey: options.QueueName, basicProperties: @event.BasicProperties, body: @event.Body);
                        }
                    }
                    else
                    {
                        if (@event.BasicProperties.Headers.ContainsKey(MessageBusConstant.FAILED_TO_QUEUE_HEADER_NAME))
                        {
                            var failedQueueName = @event.BasicProperties.Headers[MessageBusConstant.FAILED_TO_QUEUE_HEADER_NAME] as string;

                            _channel.BasicPublish(exchange: string.Empty, routingKey: failedQueueName, basicProperties: null, body: @event.Body);
                        }
                    }

                    handled = true;
                }

                if (options.PrefetchCount > 0 && handled)
                {
                    _channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
                }
            };
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                if (_channel.IsOpen)
                {
                    _channel.Close();
                }

                _channel.Dispose();
            }

            if (_connection != null)
            {
                if (_connection.IsOpen)
                {
                    _connection.Close();
                }
                _connection.Dispose();
            }
        }

        private IBasicProperties CreateDelayedRetryProperties(object delay, int numberOfRetryAttemptsLeft)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>();

            var delayInMiliseconds = delay;

            properties.Headers.Add(MessageBusConstant.DELAY_HEADER_NAME, delayInMiliseconds);
            properties.Headers.Add(MessageBusConstant.NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME, numberOfRetryAttemptsLeft - 1);

            return properties;
        }
    }
}
