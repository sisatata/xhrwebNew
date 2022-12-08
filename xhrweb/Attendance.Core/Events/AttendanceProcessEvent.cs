using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Events
{
    public class AttendanceProcessEvent : ICommand
    {
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
