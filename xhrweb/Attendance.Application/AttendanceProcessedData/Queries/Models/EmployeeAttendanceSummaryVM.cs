using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class EmployeeAttendanceSummaryVM
    {
        public Guid EmployeeId { get; set; }
        public int TotalPresent { get; set; }
        public int TotalOnTime { get; set; }
        public int TotalLate { get; set; }
        public int TotalAbsent { get; set; }
        public decimal TotalLeave { get; set; }
        public int TotalWorkingInWHdays { get; set; }
    }
}
