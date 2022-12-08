using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceRequest.Queries.Models
{
    public class AttendanceRequestNotificationVM
    {
        private string _approvalStatus;
        private int _requestTypeId;
        public Guid AttendanceApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public string RequestTypeName { get; set; }
        public string Notes { get; set; }
        public int RequestTypeId
        {
            get { return this._requestTypeId; }
            set
            {
                this._requestTypeId = value;
                RequestTypeName = ((AttendanceRequestTypes)this._requestTypeId).ToDescription();
            }
        }

        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Reason { get; set; }

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
