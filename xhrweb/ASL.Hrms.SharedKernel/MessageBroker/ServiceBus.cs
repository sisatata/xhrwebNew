using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.MessageBroker
{
    public class ServiceBus : IServiceBus
    {
        private const string Delayed_Exchange_Type = "x-delayed-message";

        private static readonly Dictionary<string, object> delayedExchangeArguments =
            new Dictionary<string, object> { { "x-delayed-type", "direct" } };

        public ServiceBus(IConfiguration configuration, ILogger<ServiceBus> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["RabbitMqConnectionString"]),
                VirtualHost = "/",
                //Protocol = Protocols.DefaultProtocol,
                ContinuationTimeout = new TimeSpan(0, 5, 2),
                AutomaticRecoveryEnabled = true
            };

            _connection = connectionFactory.CreateConnection();
        }

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly ILogger<ServiceBus> _logger;

        public async Task Publish(string queueName, object message)
        {
            await Task.Run(() =>
            {
                var jsonMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                using var channel = _connection.CreateModel();
                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
            });
        }  

        public async Task PublishWithRetry(string queueName, int numberOfRetryAttempts, TimeSpan iterationDelay, ICommandBase message, bool keepInAFailQueue = false)
        {
            if (numberOfRetryAttempts.Equals(0))
            {
                throw new ArgumentException($"Number or retry attempts must be grater than or one");
            }
            try
            {
                using var channel = _connection.CreateModel();
                _logger.LogInformation($"RabbitMQ channel open.");

                try
                {
                    var properties = channel.CreateBasicProperties();

                    properties.Headers = new Dictionary<string, object>();
                    var msgType = message.GetType().AssemblyQualifiedName;
                    properties.Headers.Add("CommandType", msgType);
                    //message.MsgCorrelationId = Guid.NewGuid();

                    if (iterationDelay != TimeSpan.Zero)
                    {
                        var delayedRetryExchangeName = string.Format(ServiceBusConstant.DELAYED_RETRY_EXCHANGE_NAME_FORMAT, queueName);

                        properties.Headers.Add(ServiceBusConstant.DELAY_HEADER_NAME, (int)iterationDelay.TotalMilliseconds);

                        channel.ExchangeDeclare(delayedRetryExchangeName, Delayed_Exchange_Type, true, false, delayedExchangeArguments);

                        channel.QueueBind(queueName, delayedRetryExchangeName, string.Empty, null);
                    }

                    if (keepInAFailQueue)
                    {
                        var failQueueName = string.Format(ServiceBusConstant.FAILED_TO_QUEUE_NAME_FORMAT, queueName);

                        properties.Headers.Add(ServiceBusConstant.FAILED_TO_QUEUE_HEADER_NAME, failQueueName);

                        channel.QueueDeclare(queue: failQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    }

                    properties.Headers.Add(ServiceBusConstant.NUMBER_OF_RETRY_ATTEMPTS_LEFT_HEADER_NAME, numberOfRetryAttempts);

                    var jsonMessage = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    await Task.Run(() => channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: properties, body: body));

                    _logger.LogInformation($"Message published with the message: !!{jsonMessage.ToString()}!!");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Message published fail: !!{ex.Message}!!");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message publish failed: {ex.Message}");
            }

        }

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.IsOpen)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _logger.LogInformation($"RabbitMQ connection disposed.");
            }
        }
    }
}
