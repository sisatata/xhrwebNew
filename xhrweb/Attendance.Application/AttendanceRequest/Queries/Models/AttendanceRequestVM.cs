using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceRequest.Queries.Models
{
    public class AttendanceRequestVM
    {
        private string _approvalStatus;
        private int _requestTypeId;
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string ApplicantName { get; set; }
        public string RequestTypeText { get; set; }
        public int RequestTypeId
        {
            get { return this._requestTypeId; }
            set
            {
                this._requestTypeId = value;
                RequestTypeText = ((AttendanceRequestTypes)this._requestTypeId).ToDescription();
            }
        }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
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
        public string Note { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
