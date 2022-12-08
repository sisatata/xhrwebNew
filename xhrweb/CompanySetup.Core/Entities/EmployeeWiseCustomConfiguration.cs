using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
   public class EmployeeWiseCustomConfiguration : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; private set; }
        public string Code { get; private set; }
        public string Value { get; private set; }
        public Guid CompanyId { get; private set; }
        public EmployeeWiseCustomConfiguration(Guid id) : base(id) { }
        private EmployeeWiseCustomConfiguration() : base(Guid.NewGuid()) { }
    }
}
