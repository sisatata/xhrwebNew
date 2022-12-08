using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class Company : BaseEntity<Guid>
    {
        public string CompanyName { get; private set; }
        public string CompanyLocalizedName { get; private set; }
        public string ShortName { get; private set; }

        public Company(Guid id) : base(id) { }
        private Company() : base(Guid.NewGuid()) { }
    }
}
