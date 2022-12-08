using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class AttendanceDataVM
    {
        public List<AttendanceProcessedDataVM> AttendanceProcessedDatas { get; set; }
        public EmployeeAttendanceSummaryVM EmployeeAttendanceSummary { get; set; }
    }
}
