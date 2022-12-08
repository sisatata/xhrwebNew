using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.MessageBroker
{
    public class ConsumerOptions
    {
        public string QueueName { get; set; }
        public ushort PrefetchCount { get; set; }
        public Func<byte[], IDictionary<string, object>, Task<bool>> MessageReceivedEventHandler { get; set; }
    }
}
