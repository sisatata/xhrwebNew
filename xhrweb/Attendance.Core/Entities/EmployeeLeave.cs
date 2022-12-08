using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Entities
{
    public class EmployeeLeave : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string LeaveTypeShortName { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CompanyId { get; set; }

        public EmployeeLeave(Guid id) : base(id) { }
        private EmployeeLeave() : base(Guid.NewGuid()) { }
    }
}

