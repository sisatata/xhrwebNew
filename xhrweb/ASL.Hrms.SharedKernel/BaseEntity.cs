using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; }
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

        protected BaseEntity(TId id)
        {

            if (typeof(TId).Name == "Guid" && object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", "id");
            }
            Id = id;
        }

        protected BaseEntity() { }
    }
}
