using System;
using ASL.Hrms.SharedKernel.Interfaces;

namespace Attendance.Core.ApplicationEvents
{
   public class AttendanceApprovedEvent : ICommand
    {
        public Guid ManagerId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string RequestType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
