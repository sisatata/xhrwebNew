using ASL.Utility.FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class ReportAttendanceProcessedDataVM : IExcelDataDynamicType
    {

        public string Date { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
      
        //public string PunchYear { get; set; }
        //public Int32? PunchMonth { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string OfficeInTime { get; set; }
        public string OfficeOutTime { get; set; }

        public string Late { get; set; }
        public string ShiftCode { get; set; }
        //public string RegularHour { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool IsBoldRow { get; set; }
        public bool WillRemoveColumn { get; set; }
    }
}
