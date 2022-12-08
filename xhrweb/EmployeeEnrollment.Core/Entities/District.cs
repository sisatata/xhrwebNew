using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class District : BaseEntity<Guid>
    {
        //public Int16? Id { get; private set; }
        public Guid DivisionId { get; private set; }
        public string Name { get; private set; }
        public string NameLocalized { get; private set; }
        public float? Latitude { get; private set; }
        public float? Longitude { get; private set; }
        public string Website { get; private set; }
        public bool IsDeleted { get; private set; }

        public District(Guid id) : base(id) { }
        private District() : base(Guid.NewGuid()) { }
    }
}

