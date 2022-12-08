using System;
using System.Collections.Generic;
using System.Text;
namespace Attendance.Application.ReportPrint.Model
{
   public class GroupedAttendanceSummaryPdfVM
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string JoiningDateString { get; set; }
        public string TotalOT { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeCode { get; set; }
        public string DesignationName { get; set; }
        public short? TotalPresentDays { get; set; }
        public short? TotalAbsentDays { get; set; }
        public short? TotalLeaveDays { get; set; }
        public short? TotalLateDays { get; set; }
        public Guid? EmployeeId { get; set; }
        public string DayName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string BranchName { get; set; }
        public List<ReportAttendanceSummaryPdfVM> DataList { get; set; }
    }
}
