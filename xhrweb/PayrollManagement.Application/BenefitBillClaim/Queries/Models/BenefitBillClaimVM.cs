using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Queries.Models
{
    public class BenefitBillClaimVM
    {
        private string _approvalStatus;
        public Guid Id { get; set; }
        public Guid? BenefitDeductionId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? ClaimDate { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public decimal? ClaimAmount { get; set; }
        public string Remarks { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedNotes { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
        public string BillAttachmentUri { get; set; }
        public string BillAttachmentName { get; set; }
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
        public Guid? PaidBy { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? PaymentStatus { get; set; }
        public Guid? SettledBy { get; set; }
        public DateTime? SettledDate { get; set; }

        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string NameOfAttendees { get; set; }
        public int NumberOfAttendees { get; set; }
        public Guid? VehicleTypeId { get; set; }

        public string VehicleTypeName { get; set; }

    }
}
