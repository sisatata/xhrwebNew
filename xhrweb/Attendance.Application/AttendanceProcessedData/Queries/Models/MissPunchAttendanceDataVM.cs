using ASL.Utility.FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class MissPunchAttendanceDataVM 
    {

        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string OfficeInTime { get; set; }
        public string OfficeOutTime { get; set; }

        public string Late { get; set; }
        public string ShiftCode { get; set; }
        
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string EmployeeEmail { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public string ManagerEmail { get; set; }
    }
}
