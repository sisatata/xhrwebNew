using ASL.Hrms.Api.HostedServices;
using ASL.Hrms.Api.HostedServices.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ASL.Hrms.Api.ExtensionMethods
{
    public static class MessageBrokerDependencyInjection
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection serviceCollection, IEnumerable<Assembly> commandHandlersAssemblies)
        {
            serviceCollection.AddScoped<IServiceBus, ServiceBus>();
            serviceCollection.AddSingleton<IMessageConsumer, MessageConsumer>();
            serviceCollection.AddSingleton<IHostedService, MessageBrokerHostedService>();
            serviceCollection.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();

            var commandHandlerBaseType = typeof(ICommandHandler<>);

            foreach (var type in commandHandlersAssemblies.SelectMany(x => x.ExportedTypes).Where(x => x.IsAssignableToGenericType(commandHandlerBaseType)))
                serviceCollection.AddTransient(type.GetCommandHandlerInterface(), type);

            return serviceCollection;
        }
    }
}
