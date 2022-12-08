using System;


namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class AttendanceProcessedCommandVM
    {
       
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class AttendanceProcessedHangfireBackgroundCommandVM
    {
        public Guid CompanyId { get; set; }
        public AttendanceProcessedCommandVM AttendanceAttendanceProcessedResult { get; set; }
    }
}
