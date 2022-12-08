using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities
{
    public class CompanyWiseConfiguration : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public Guid CompanyId { get; set; }
    }
}
