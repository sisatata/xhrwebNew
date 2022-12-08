using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
    public class EmployeeCustomConfiguration : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Value { get; set; }
        //public string Description { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public Boolean IsDeleted { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
    }
}