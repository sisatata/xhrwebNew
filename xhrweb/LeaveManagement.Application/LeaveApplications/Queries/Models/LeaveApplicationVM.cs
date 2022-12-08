using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveApplications.Queries.Models
{
    public class LeaveApplicationVM
    {
        private string _approvalStatus;
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveTypeName { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string LeaveCalendar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public string ApprovalStatusText { get; set; }
        public string ApprovalStatus
        {
            get { return this._approvalStatus; }
            set
            {
                this._approvalStatus = value;
                ApprovalStatusText = ((ApprovalStatuses)Convert.ToInt32(!string.IsNullOrEmpty(this._approvalStatus) ? this._approvalStatus : "1")).ToString();
            }
        }

        public string Notes { get; set; }
        public double LeaveDays { get; set; }
        public string Reason { get; set; }

        public string AddressOnLeave { get; set; }
        public string EmergencyContact { get; set; }
        public Boolean IsHalfDayLeave { get; set; }
        public int? HalfDayLeaveTypeId { get; set; }
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
