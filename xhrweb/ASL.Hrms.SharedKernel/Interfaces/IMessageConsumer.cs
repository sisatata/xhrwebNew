using ASL.Hrms.SharedKernel.MessageBroker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IMessageConsumer : IDisposable
    {
        event EventHandler<DisconnectedEventArgs> Disconnected;
        bool Connect();
        bool Connect(string rabbitMqConnectionString);
        void Disconnect();
        void Subscribe(ConsumerOptions options);
    }
}
