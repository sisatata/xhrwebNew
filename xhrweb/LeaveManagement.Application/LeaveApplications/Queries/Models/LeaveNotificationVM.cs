using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveApplications.Queries.Models
{
    public class LeaveNotificationVM
    {
        private string _approvalStatus;
        public Guid LeaveApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public decimal NoOfDays { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeShortName { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Notes { get; set; }
        public decimal MaximumBalance { get; set; }
        public decimal UsedBalance { get; set; }
        public decimal RemainingBalance { get; set; }
        public string Reason { get; set; }
        public string AddressOnLeave { get; set; }
        public string EmergencyContact { get; set; }
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
    }
}
