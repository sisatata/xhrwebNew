using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Queries.Models
{
    public class BillClaimNotificationVM
    {
        //public BillClaimNotificationVM(IUriComposer uriComposer)
        //{
        //    _uriComposer = uriComposer;
        //}


        //private readonly IUriComposer _uriComposer;
        private string _approvalStatus;
        private string _billAttachmentUri;
        public Guid BenefitBillClaimId { get; set; }
        public string ApplicantName { get; set; }
        public Guid? BenefitDeductionId { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? ClaimDate { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public decimal? ClaimAmount { get; set; }
        public string Remarks { get; set; }

        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public string BillAttachmentUri { get; set; }

        public string BillAttachmentName { get; set; }
        //{
        //    get { return this._billAttachmentUri; }
        //    set
        //    {
        //        this._billAttachmentUri = value;
        //        BillAttachmentUri = !string.IsNullOrEmpty(this._billAttachmentUri) ? $"{_planetHRSettings.BaseUrl}/{_planetHRSettings.ContentRoot.EmployeeImagePath}{this._billAttachmentUri}" : "";
        //    }
        //}
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
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string CompanyEmployeeId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }

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
