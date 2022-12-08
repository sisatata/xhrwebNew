using System;


namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class AttendanceProcessedDataCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
