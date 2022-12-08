using ASL.Utility.FileManager.Interfaces;
using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PayrollManagement.Application.BenefitBillClaim.Queries.Models
{
    public class ReportBenefitBillClaimVM : IExcelDataDynamicType
    {
        private int? _paymentStatus;
        [Description("Employee Id")]
        public string EmployeeId { get; set; }
        [Description("Applicant Name")]
        public string ApplicantName { get; set; }
        [Description("Bill Type")]
        public string BillType { get; set; }
        [Description("Bill Date")]
        public string BillDate { get; set; }
        [Description("Claim Date")]
        public string ClaimDate { get; set; }
        [Description("Allocated Amount")]
        public decimal? AllocatedAmount { get; set; }
        [Description("Claim Amount")]
        public decimal? ClaimAmount { get; set; }
        [Description("Remarks")]
        public string Remarks { get; set; }
        [Description("Approved Amount")]
        public decimal? ApprovedAmount { get; set; }
        [Description("Approval Status")]
        public string ApprovalStatus { get; set; }
        [Description("Approved Date")]
        public string ApprovedDate { get; set; }
        [Description("Note")]
        public string Note { get; set; }
        [Description("Manager Name")]
        public string ManagerName { get; set; }
        public bool IsBoldRow { get; set; }
        public bool WillRemoveColumn { get; set; }
        [Description("Location From")]
        public string LocationFrom { get; set; }
        [Description("Location To")]
        public string LocationTo { get; set; }
        [Description("Vehicle Type")]
        public string VehicleTypeId { get; set; }
        [Description("Number Of Attendees")]
        public int? NumberOfAttendees { get; set; }
        [Description("Name Of Attendees")]
        public string NameOfAttendees { get; set; }
        [Description("Paid By")]
        public string PaidBy { get; set; }
        [Description("Paid Date")]
        public string PaidDate { get; set; }
        [Description("Payment Status")]
        public string PaymentStatusText { get; set; }
        //[Description("Payment Status")]
        public int PaymentStatus
        {
            get { return this._paymentStatus.Value; }
            set
            {
                this._paymentStatus = value;
                PaymentStatusText = this._paymentStatus.HasValue ? ((PaymentStatuses)this._paymentStatus).ToString() : "";
            }
        }
        [Description("Settled By")]
        public string SettledBy { get; set; }
        [Description("Settled Date")]
        public string SettledDate { get; set; }
    }
}
