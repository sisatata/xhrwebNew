using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.AdminDashBoard.Queries.Models
{
    public class DailyAttendanceSummaryVM
    {
        public DateTime PunchDate { get; set; }
        public int TotalPresent { get; set; }
        public int TotalLate { get; set; }
        public int TotalAbsent { get; set; }
        public double TotalLeave { get; set; }
    }
}
