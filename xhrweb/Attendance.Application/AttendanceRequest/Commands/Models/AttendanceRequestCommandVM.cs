using System;


namespace Attendance.Application.AttendanceRequest.Commands
{
    public class AttendanceRequestCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
