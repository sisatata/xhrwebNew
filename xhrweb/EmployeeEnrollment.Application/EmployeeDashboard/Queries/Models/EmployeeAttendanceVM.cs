using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models
{
    public class DailyAttendanceSummaryVM
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
