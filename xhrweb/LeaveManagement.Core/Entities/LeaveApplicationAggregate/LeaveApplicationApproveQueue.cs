using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace LeaveManagement.Core.Entities.LeaveApplicationAggregate
{
    public class LeaveApplicationApproveQueue : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? LeaveApplicationId { get;  set; }
        public Guid? ManagerId { get;  set; }
        public string ApprovalStatus { get;  set; }
        public DateTime? ApprovedTime { get;  set; }
        public string Note { get;  set; }


        public LeaveApplicationApproveQueue(Guid id) : base(id) { }
        private LeaveApplicationApproveQueue() : base(Guid.NewGuid()) { }

        public static LeaveApplicationApproveQueue Create(

         Guid? leaveApplicationId,
         Guid? managerId,
         string approvalStatus,
         DateTime? approvedTime,
         string note

        )

        {
            var oModel = new LeaveApplicationApproveQueue(Guid.NewGuid());

            oModel.LeaveApplicationId = leaveApplicationId;
            oModel.ManagerId = managerId;
            oModel.ApprovalStatus = approvalStatus;
            oModel.ApprovedTime = approvedTime;
            oModel.Note = note;

            return oModel;

        }


        public void UpdateLeaveApplicationApproveQueue
            (

         Guid? leaveApplicationId,
         Guid? managerId,
         string approvalStatus,
         DateTime? approvedTime,
         string note

        )
        {
            LeaveApplicationId = leaveApplicationId;
            ManagerId = managerId;
            ApprovalStatus = approvalStatus;
            ApprovedTime = approvedTime;
            Note = note;
        }


        //public void MarkAsDeleteLeaveApplicationApproveQueue()
        //{
        //    IsDeleted = true;
        //}


    }
}

