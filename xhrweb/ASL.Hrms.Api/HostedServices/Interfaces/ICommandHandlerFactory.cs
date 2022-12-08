using ASL.Hrms.SharedKernel.Interfaces;
using System;

namespace ASL.Hrms.Api.HostedServices.Interfaces
{
    public interface ICommandHandlerFactory
    {
        IBaseCommandHandler Get(Type key);
    }

    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        private readonly IServiceProvider _serviceProvider;
        public IBaseCommandHandler Get(Type key) => (IBaseCommandHandler)_serviceProvider.GetService(key);
    }
}
