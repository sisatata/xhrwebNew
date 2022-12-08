using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Entities
{
    public class EmployeeCustomConfiguration : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
    }
}

