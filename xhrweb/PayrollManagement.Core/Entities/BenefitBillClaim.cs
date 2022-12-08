using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitBillClaim : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {

        public Guid? BenefitDeductionId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public DateTime? BillDate { get; private set; }
        public DateTime? ClaimDate { get; private set; }
        public decimal? AllocatedAmount { get; private set; }
        public decimal? ClaimAmount { get; private set; }
        public string Remarks { get; private set; }
        public decimal? ApprovedAmount { get; private set; }
        public Guid? ApprovedBy { get; private set; }
        public DateTime? ApprovedDate { get; private set; }
        public string ApprovedNotes { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; private set; }
        public string BillAttachmentName { get; private set; }
        public string LocationFrom { get; private set; }
        public string LocationTo { get; private set; }
        public Guid? VehicleTypeId { get; private set; }
        public int? NumberOfAttendees { get; private set; }
        public string NameOfAttendees { get; private set; }

        public int PaymentStatus { get; private set; }
        public Guid? PaidBy { get; private set; }
        public DateTime? PaidDate { get; private set; }

        public Guid? SettledBy { get; private set; }
        public DateTime? SettledDate { get; private set; }
        public int BillNo { get; set; }
        public string BillNoMaskingValue { get; set; }
        public BenefitBillClaim(Guid id) : base(id) { }
        private BenefitBillClaim() : base(Guid.NewGuid()) { }

        public static BenefitBillClaim Create(

         Guid? benefitDeductionId,
         Guid? employeeId,
         DateTime? billDate,
         DateTime? claimDate,
         decimal? allocatedAmount,
         decimal? claimAmount,
         string remarks,
         Boolean isDeleted,
         Guid? companyId,
         string status,
         string approvalStatus,
         string billAttachmentName,
         string locationFrom,
         string locationTo,
         Guid? vehicleTypeId,
         int? numberOfAttendees,
         string nameOfAttendees

        )

        {
            var oModel = new BenefitBillClaim(Guid.NewGuid());

            oModel.BenefitDeductionId = benefitDeductionId;
            oModel.EmployeeId = employeeId;
            oModel.BillDate = billDate;
            oModel.ClaimDate = claimDate;
            oModel.AllocatedAmount = allocatedAmount;
            oModel.ClaimAmount = claimAmount;
            oModel.Remarks = remarks;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;
            oModel.Status = status;
            oModel.ApprovalStatus = approvalStatus;
            oModel.BillAttachmentName = billAttachmentName;
            oModel.LocationFrom = locationFrom;
            oModel.LocationTo = locationTo;
            oModel.VehicleTypeId = vehicleTypeId;
            oModel.NumberOfAttendees = numberOfAttendees;
            oModel.NameOfAttendees = nameOfAttendees;
            return oModel;

        }


        public void UpdateBenefitBillClaim
            (

         Guid? benefitDeductionId,
         Guid? employeeId,
         DateTime? billDate,
         DateTime? claimDate,
         decimal? allocatedAmount,
         decimal? claimAmount,
         string remarks,
         Boolean isDeleted,
         Guid? companyId,
         string status,
         string approvalStatus,
         string billAttachmentName,
         string locationFrom,
         string locationTo,
         Guid? vehicleTypeId,
         int? numberOfAttendees,
         string nameOfAttendees
        )
        {
            BenefitDeductionId = benefitDeductionId;
            EmployeeId = employeeId;
            BillDate = billDate;
            ClaimDate = claimDate;
            AllocatedAmount = allocatedAmount;
            ClaimAmount = claimAmount;
            Remarks = remarks;
            IsDeleted = isDeleted;
            CompanyId = companyId;
            Status = status;
            ApprovalStatus = approvalStatus;
            if (!string.IsNullOrEmpty(billAttachmentName))
            {
                BillAttachmentName = billAttachmentName;
            }
            LocationFrom = locationFrom;
            LocationTo = locationTo;
            VehicleTypeId = vehicleTypeId;
            NumberOfAttendees = numberOfAttendees;
            NameOfAttendees = nameOfAttendees;
        }

        public void ApproveBenefitBillClaim
            (
         decimal? approvedAmount,
         Guid? approvedBy,
         DateTime? approvedDate,
         string approvedNotes,
         string approvalStatus
        )
        {
            ApprovedAmount = approvedAmount;
            ApprovedBy = approvedBy;
            ApprovedDate = approvedDate;
            ApprovedNotes = approvedNotes;
            ApprovalStatus = approvalStatus;// ((int)ApprovalStatuses.Approved).ToString();
        }

        public void RejectBenefitBillClaim
           (
        Guid? approvedBy,
        DateTime? approvedDate,
        string approvedNotes,
        string approvalStatus
       )
        {
            ApprovedBy = approvedBy;
            ApprovedDate = approvedDate;
            ApprovedNotes = approvedNotes;
            ApprovalStatus = approvalStatus;// ((int)ApprovalStatuses.Declined).ToString();
        }

        public void MarkAsDeleteBenefitBillClaim()
        {
            IsDeleted = true;
        }

        public void UpdateBillAttachment(string billAttachmentName)
        {
            Guard.Against.NullOrWhiteSpace(billAttachmentName, "BillAttachmentName");

            BillAttachmentName = billAttachmentName;
        }

        public void MarkAsUnPaidBenefitBillClaim
           (
        Guid? paidBy,
        int paymentStatus
       )
        {
            PaidBy = paidBy;
            PaymentStatus = paymentStatus;
        }

        public void MarkAsPaidBenefitBillClaim
           (
        Guid? paidBy,
        DateTime? paidDate,
        int paymentStatus
       )
        {
            PaidBy = paidBy;
            PaidDate = paidDate;
            PaymentStatus = paymentStatus;
        }

        public void MarkAsSettledBenefitBillClaim
           (
        Guid? settleBy,
        DateTime? settleDate,
        int paymentStatus
       )
        {
            SettledBy = settleBy;
            SettledDate = settleDate;
            PaymentStatus = paymentStatus;
        }
    }
}

