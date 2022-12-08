using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary.Models
{
   public class LeaveBalanceSummaryWithSearch
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveCalendar { get; set; }
        public double MaximumBalance { set; get; }
        public double UsedBalance { set; get; }
        public double RemainingBalance { set; get; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PositionId { get; set; }
        public string DesignationName { get; set; }
        public string CompanyEmployeeId { get; set; }
        public string LoginId { get; set; }

    }
}
