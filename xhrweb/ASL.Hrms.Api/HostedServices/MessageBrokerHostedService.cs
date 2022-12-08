using ASL.Hrms.Api.HostedServices.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.HostedServices
{
    public class MessageBrokerHostedService : IHostedService
    {
        public MessageBrokerHostedService(IConfiguration configuration, ILogger<MessageBrokerHostedService> logger,
            IMessageConsumer messageConsumer,
            IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _messageConsumer = messageConsumer;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageBrokerHostedService> _logger;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private ICommandHandlerFactory _handlerFactory;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartService();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageConsumer.Disconnect();
            return Task.CompletedTask;
        }

        private void StartService()
        {
            _logger.LogInformation("Starting Message Broker Service");

            _messageConsumer.Disconnected += ConsumerDisconnected;

            var connected = _messageConsumer.Connect(_configuration["RabbitMqConnectionString"]);

            if (!connected)
            {
                _logger.LogInformation($"Service is unable to connect to RabbitMQ.\nParameters: {_configuration["RabbitMqConnectionString"]}");
                return;
            }

            _logger.LogInformation("Service connected to RabbitMQ");

            ushort prefetchCount = _configuration.GetValue<ushort>("PrefetchCount");

            var subscriptionOptions = new ConsumerOptions
            {
                PrefetchCount = prefetchCount,
                MessageReceivedEventHandler = EventReceived,
                QueueName = _configuration.GetValue<string>("TransactionQueueName")
            };

            _messageConsumer.Subscribe(subscriptionOptions);
        }

        private async Task<bool> EventReceived(byte[] messageBody, IDictionary<string, object> headers)
        {
            var message = Encoding.UTF8.GetString(messageBody, 0, messageBody.Length);
            _logger.LogInformation($"published message body: {JsonConvert.SerializeObject(message)}");

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    _handlerFactory = scope.ServiceProvider.GetRequiredService<ICommandHandlerFactory>();


                    var byteArray = headers.SingleOrDefault(h => h.Key == "CommandType").Value as byte[];
                    Type eventType = Type.GetType(Encoding.UTF8.GetString(byteArray));
                    var rawObject = JsonConvert.DeserializeObject(message, eventType);

                    var genericCommandHandlerType = typeof(ICommandHandler<>);
                    var requestCommandHandlerType = genericCommandHandlerType.MakeGenericType(eventType);

                    var handler = (dynamic)_handlerFactory.Get(requestCommandHandlerType);
                    var handleResult = (dynamic)handler.Handle((dynamic)rawObject);

                    await handleResult;
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return true;
        }

        private void ConsumerDisconnected(object sender, DisconnectedEventArgs e)
        {
            _logger.LogInformation($"Service is disconnected from RabbitMQ.\nEvent arguments Cause: {e.Cause} Initiator: {e.Initiator} ReplyCode: {e.ReplyCode} ReplyText: {e.ReplyText}");
        }
    }
}
