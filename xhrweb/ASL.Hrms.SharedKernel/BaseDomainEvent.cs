using System;

namespace ASL.Hrms.SharedKernel
{
    public abstract class BaseDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
