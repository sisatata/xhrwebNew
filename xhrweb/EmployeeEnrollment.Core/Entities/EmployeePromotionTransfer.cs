using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.ExtensionMethods;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeePromotionTransfer : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? PreviousCompanyId { get; private set; }
        public Guid? PreviousBranchId { get; private set; }
        public Guid? PreviousDepartmentId { get; private set; }
        public Guid? PreviousPositionId { get; private set; }
        public Guid? NewCompanyId { get; private set; }
        public Guid? NewBranchId { get; private set; }
        public Guid? NewDepartmentId { get; private set; }
        public Guid? NewPositionId { get; private set; }
        public DateTime? ProposedDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal? PreviousGross { get; private set; }
        public decimal? NewGross { get; private set; }
        public decimal? PreviousBasic { get; private set; }
        public decimal? NewBasic { get; private set; }
        public decimal? PreviousHouserent { get; private set; }
        public decimal? NewHouserent { get; private set; }
        public Int32? IncrementTypeId { get; private set; }
        public Int32? IncrementValueTypeId { get; private set; }
        public decimal? IncrementValue { get; private set; }
        public decimal? IncrementAmount { get; private set; }
        public Int32? IncrementOn { get; private set; }
        public Int32? IncrementOnId { get; private set; }
        public string Reason { get; private set; }
        public string Reference { get; private set; }
        public DateTime? ApproveDate { get; private set; }
        public Guid? ApproveBy { get; private set; }
        public string ApproveNote { get; private set; }
        public string ApprovalStatus { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public Guid? PreviousGradeId { get; private set; }
        public Guid? NewGradeId { get; private set; }
        public Guid? PreviousSalaryStructureId { get; private set; }
        public Guid? NewSalaryStructureId { get; private set; }
        public short? PreviousPaymentOptionId { get; private set; }
        public short? NewPaymentOptionId { get; private set; }

        public EmployeePromotionTransfer(Guid id) : base(id) { }
        private EmployeePromotionTransfer() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         Guid? previousCompanyId,
         Guid? previousBranchId,
         Guid? previousDepartmentId,
         Guid? previousPositionId,
         Guid? newCompanyId,
         Guid? newBranchId,
         Guid? newDepartmentId,
         Guid? newPositionId,
         DateTime? proposedDate,
         DateTime startDate,
         DateTime? endDate,
         decimal? previousGross,
         decimal? newGross,
         decimal? previousBasic,
         decimal? newBasic,
         decimal? previousHouserent,
         decimal? newHouserent,
         Int32? incrementTypeId,
         Int32? incrementValueTypeId,
         decimal? incrementValue,
         decimal? incrementAmount,
         Int32? incrementOn,
         string reason,
         string reference,

         Guid? previousGradeId,
         Guid? newGradeId,
         Guid? previousSalaryStructureId,
         Guid? newSalaryStructureId,
         short? previousPaymentOptionId,
         short? newPaymentOptionId
        )

        {
            //var oModel = new EmployeePromotionTransfer(Guid.NewGuid());

            this.EmployeeId = employeeId;
            this.PreviousCompanyId = previousCompanyId;
            this.PreviousBranchId = previousBranchId;
            this.PreviousDepartmentId = previousDepartmentId;
            this.PreviousPositionId = previousPositionId;
            this.NewCompanyId = newCompanyId;
            this.NewBranchId = newBranchId;
            this.NewDepartmentId = newDepartmentId;
            this.NewPositionId = newPositionId;
            this.ProposedDate = proposedDate;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.PreviousGross = previousGross;
            this.NewGross = newGross;
            this.PreviousBasic = previousBasic;
            this.NewBasic = newBasic;
            this.PreviousHouserent = previousHouserent;
            this.NewHouserent = newHouserent;
            this.IncrementTypeId = incrementTypeId;
            this.IncrementValueTypeId = incrementValueTypeId;
            this.IncrementValue = incrementValue;
            this.IncrementAmount = incrementAmount;
            this.IncrementOn = incrementOn;
            this.Reason = reason;
            this.Reference = reference;
            this.PreviousGradeId = previousGradeId;
            this.NewGradeId = newGradeId;
            this.PreviousSalaryStructureId = previousSalaryStructureId;
            this.NewSalaryStructureId = newSalaryStructureId;
            this.PreviousPaymentOptionId = previousPaymentOptionId;
            this.NewPaymentOptionId = newPaymentOptionId;

            this.IsDeleted = false;
            //return oModel;

        }


        public void UpdateEmployeePromotionTransfer
            (

         Guid? employeeId,
         Guid? previousCompanyId,
         Guid? previousBranchId,
         Guid? previousDepartmentId,
         Guid? previousPositionId,
         Guid? newCompanyId,
         Guid? newBranchId,
         Guid? newDepartmentId,
         Guid? newPositionId,
         DateTime? proposedDate,
         DateTime startDate,
         DateTime? endDate,
         decimal? previousGross,
         decimal? newGross,
         decimal? previousBasic,
         decimal? newBasic,
         decimal? previousHouserent,
         decimal? newHouserent,
         Int32? incrementTypeId,
         decimal? incrementValue,
         decimal? incrementAmount,
         Int32? incrementOn,
         string reason,
         string reference,

         Guid? previousGradeId,
         Guid? newGradeId,
         Guid? previousSalaryStructureId,
         Guid? newSalaryStructureId,
         short? previousPaymentOptionId,
         short? newPaymentOptionId
        )
        {
            EmployeeId = employeeId;
            PreviousCompanyId = previousCompanyId;
            PreviousBranchId = previousBranchId;
            PreviousDepartmentId = previousDepartmentId;
            PreviousPositionId = previousPositionId;
            NewCompanyId = newCompanyId;
            NewBranchId = newBranchId;
            NewDepartmentId = newDepartmentId;
            NewPositionId = newPositionId;
            ProposedDate = proposedDate;
            StartDate = startDate;
            EndDate = endDate;
            PreviousGross = previousGross;
            NewGross = newGross;
            PreviousBasic = previousBasic;
            NewBasic = newBasic;
            PreviousHouserent = previousHouserent;
            NewHouserent = newHouserent;
            IncrementTypeId = incrementTypeId;
            IncrementValue = incrementValue;
            IncrementAmount = incrementAmount;
            IncrementOn = incrementOn;
            Reason = reason;
            Reference = reference;

            PreviousGradeId = previousGradeId;
            NewGradeId = newGradeId;
            PreviousSalaryStructureId = previousSalaryStructureId;
            NewSalaryStructureId = newSalaryStructureId;
            PreviousPaymentOptionId = previousPaymentOptionId;
            NewPaymentOptionId = newPaymentOptionId;

            IsDeleted = false;
        }


        public void MarkAsDeleteEmployeePromotionTransfer()
        {
            IsDeleted = true;
        }

        public void ApproveEmployeePromotionTransfer(string notes = "")
        {
            Guard.Against.InvalidPromotionStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), nameof(ApprovalStatuses), "already approved application");
            ApprovalStatus = ((int)ApprovalStatuses.Approved).ToString();// LeaveApplicationStatus.Approved;
            ApproveNote = notes;
        }

        public void RejectEmployeePromotionTransfer(string notes = "")
        {
            Guard.Against.InvalidPromotionStatus(ApprovalStatus, ((int)ApprovalStatuses.Approved).ToString(), nameof(ApprovalStatuses), "already approved application");
            Guard.Against.InvalidPromotionStatus(ApprovalStatus, ((int)ApprovalStatuses.Declined).ToString(), nameof(ApprovalStatuses), "already declined application");
            ApprovalStatus = ((int)ApprovalStatuses.Declined).ToString();// LeaveApplicationStatus.Rejected;
            ApproveNote = notes;
        }
    }
}

