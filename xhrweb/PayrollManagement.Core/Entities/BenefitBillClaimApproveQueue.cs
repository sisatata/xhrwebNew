using ASL.Hrms.SharedKernel;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitBillClaimApproveQueue : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? BenefitBillClaimId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public decimal? ApprovedAmount { get;  set; }
        public string ApprovalStatus { get;  set; }
        public DateTime? ApprovedDate { get;  set; }
        public string Note { get;  set; }


        public BenefitBillClaimApproveQueue(Guid id) : base(id) { }
        private BenefitBillClaimApproveQueue() : base(Guid.NewGuid()) { }

        public static BenefitBillClaimApproveQueue Create(

         Guid? benefitBillClaimId,
         Guid? managerId,
         decimal? approvedAmount,
         string approvalStatus,
         DateTime? approvedDate,
         string note

        )

        {
            var oModel = new BenefitBillClaimApproveQueue(Guid.NewGuid());

            oModel.BenefitBillClaimId = benefitBillClaimId;
            oModel.ManagerId = managerId;
            oModel.ApprovedAmount = approvedAmount;
            oModel.ApprovalStatus = approvalStatus;
            oModel.ApprovedDate = approvedDate;
            oModel.Note = note;

            return oModel;

        }


        public void UpdateBenefitBillClaimApproveQueue
            (

         Guid? benefitBillClaimId,
         Guid? managerId,
         decimal? approvedAmount,
         string approvalStatus,
         DateTime? approvedDate,
         string note
    
        )
        {
            BenefitBillClaimId = benefitBillClaimId;
            ManagerId = managerId;
            ApprovedAmount = approvedAmount;
            ApprovalStatus = approvalStatus;
            ApprovedDate = approvedDate;
            Note = note;
        }


        //public void MarkAsDeleteBenefitBillClaimApproveQueue()
        //{
        //    IsDeleted = true;
        //}


    }
}

