using System;


namespace Attendance.Application.Attendance.Commands
{
    public class AttendanceCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
