using ASL.Hrms.SharedKernel;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class AttendanceRequestApproveQueue : BaseEntity<Guid>, IAggregateRoot
    {
        //public Guid? Id { get; private set; }
        public Guid? AttendanceApplicationId { get;  set; }
        public Guid? ManagerId { get;  set; }
        public string ApprovalStatus { get;  set; }
        public DateTime? ApprovedDate { get;  set; }
        public string Note { get;  set; }


        public AttendanceRequestApproveQueue(Guid id) : base(id) { }
        private AttendanceRequestApproveQueue() : base(Guid.NewGuid()) { }

        public static AttendanceRequestApproveQueue Create(

         //Guid? id,
         Guid? attendanceApplicationId,
         Guid? managerId,
         string approvalStatus,
         DateTime? approvedDate,
         string note

        )

        {
            var oModel = new AttendanceRequestApproveQueue(Guid.NewGuid());

            //oModel.Id = id;
            oModel.AttendanceApplicationId = attendanceApplicationId;
            oModel.ManagerId = managerId;
            oModel.ApprovalStatus = approvalStatus;
            oModel.ApprovedDate = approvedDate;
            oModel.Note = note;

            return oModel;

        }


        public void UpdateAttendanceRequestApproveQueue
            (

         Guid? id,
         Guid? attendanceApplicationId,
         Guid? managerId,
         string approvalStatus,
         DateTime? approvedDate,
         string note

        )
        {

            AttendanceApplicationId = attendanceApplicationId;
            ManagerId = managerId;
            ApprovalStatus = approvalStatus;
            ApprovedDate = approvedDate;
            Note = note;
        }


        //public void MarkAsDeleteAttendanceRequestApproveQueue()
        //{
        //    IsDeleted = true;
        //}


    }
}

