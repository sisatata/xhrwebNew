using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveApplications.Queries.Models
{
    public class LeaveDataForEmailVM
    {
        private string _approvalStatus;
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string EmployeeEmail { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }
        public string ManagerEmail { get; set; }

        public string LeaveTypeShortName { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TodayDate { get; set; }
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
        public int? HalfDayLeaveTypeId { get; set; }
    }
}
