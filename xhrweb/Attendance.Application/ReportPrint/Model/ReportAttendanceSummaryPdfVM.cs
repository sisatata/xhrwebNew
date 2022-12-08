using System;
using System.Collections.Generic;
using System.Text;
namespace Attendance.Application.ReportPrint.Model
{
    #region Properties ReportAttendanceSummaryPdfVM
    public class ReportAttendanceSummaryPdfVM
    {

        public string FullName { get; set; }
        public string TotalOT { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogoUri { get; set; }
        public Guid? BranchId { get; set; }

        public Guid? EID { get; set; }


        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public short? TotalLateDays { get; set; }
        public short? TotalPresentDays { get; set; }
        public int? DesignationOrder { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public short? TotalAbsentDays { get; set; }
        public short? TotalLeaveDays { get; set; }
        public Guid? EmployeeId { get; set; }
        public string JoiningDateString { get; set; }
        // public string CompanyName { get; set; }
        public DateTime? PunchDate { get; set; }
        public string PunchYear { get; set; }
        public int? PunchMonth { get; set; }
        public Guid? Id { get; set; }

        //  public string AddressLine1 { get; set; }

        public string EmployeeCode { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string Status { get; set; }
        public string Status_V2 { get; set; }
       public string DayName { get; set; }


        public DateTime? OTHour { get; set; }
    }
    #endregion
    #region Properties CompanyNameAndAddressVM
    public class CompanyNameAndAddressVM
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
    }
    #endregion
}
