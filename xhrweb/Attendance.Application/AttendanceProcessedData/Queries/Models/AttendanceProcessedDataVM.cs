using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceProcessedData.Queries.Models
{
    public class AttendanceProcessedDataVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeCompanyId { get; set; }
        public string EmployeeName { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PositionId { get; set; }
        public string DesignationName { get; set; }

        public string LoginId { get; set; }
        public DateTime PunchDate { get; set; }
        public string PunchYear { get; set; }
        public Int32? PunchMonth { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? ShiftIn { get; set; }
        public DateTime? ShiftOut { get; set; }
        public DateTime? BreakIn { get; set; }
        public DateTime? BreakOut { get; set; }
        public DateTime? BreakLate { get; set; }
        public DateTime? Late { get; set; }
        public Guid? ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public DateTime? RegularHour { get; set; }
        public DateTime? OTHour { get; set; }
        public string Status { get; set; }
        public string Status_V2 { get; set; }
        public DateTime? BuyerShiftIn { get; set; }
        public DateTime? BuyerShiftOut { get; set; }
        public DateTime? BuyerOTTime { get; set; }
        public string Remarks { get; set; }
        public Boolean IsLunchOut { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Guid? MonthCycleId { get; set; }
        public decimal? TimeOutLatitude { get; set; }
        public decimal? TimeOutLongitude { get; set; }
        public decimal? TimeInLatitude { get; set; }
        public decimal? TimeInLongitude { get; set; }
        public string EmployeeRemarks { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
    }
}
