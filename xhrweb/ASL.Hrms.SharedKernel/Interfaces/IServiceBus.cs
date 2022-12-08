using System;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IServiceBus : IDisposable
    {
        Task Publish(string queueName, object message);
        Task PublishWithRetry(string queueName, int numberOfRetryAttempts, TimeSpan iterationDelay, ICommandBase message, bool keepInAFailQueue = false);
    }
}
