using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class AttendanceRequest : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid EmployeeId { get; set; }
        //public Guid? RequestTypeId { get; private set; }
        public int RequestTypeId { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Reason { get; private set; }
        public string ApprovalStatus { get; private set; }
        public string Note { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }


        public AttendanceRequest(Guid id) : base(id) { }
        private AttendanceRequest() : base(Guid.NewGuid()) { }

        public static AttendanceRequest Create(
            Guid employeeId,
         int requestTypeId,
         DateTime? startTime,
         DateTime? endTime,
         string reason,
         Guid companyId,
         string approvalStatus

        )

        {
            var oModel = new AttendanceRequest(Guid.NewGuid());
            oModel.EmployeeId = employeeId;
            oModel.RequestTypeId = requestTypeId;
            oModel.StartTime = startTime;
            oModel.EndTime = endTime;
            oModel.Reason = reason;
            oModel.CompanyId = companyId;
            oModel.ApprovalStatus = approvalStatus;
            return oModel;

        }


        public void UpdateAttendanceRequest
            (
            Guid employeeId,
         int requestTypeId,
         DateTime? startTime,
         DateTime? endTime,
         string reason,
         Guid companyId,
         string approvalStatus

        )
        {
            EmployeeId = employeeId;
            RequestTypeId = requestTypeId;
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason;
            CompanyId = companyId;
            ApprovalStatus = approvalStatus;
        }

        public void ApproveAttendanceRequest
            (
             //Guid employeeId,
             //int requestTypeId,
             //DateTime? startTime,
             //DateTime? endTime,
             //string reason,
             //string approvalStatus,
             string note
        //Guid companyId

        )
        {
            //EmployeeId = employeeId;
            //RequestTypeId = requestTypeId;
            //StartTime = startTime;
            //EndTime = endTime;
            //Reason = reason;
            ApprovalStatus = ((int)ApprovalStatuses.Approved).ToString();
            Note = note;
            //CompanyId = companyId;
        }

        public void RejectAttendanceRequest
           (
            string note
       )
        {
            ApprovalStatus = ((int)ApprovalStatuses.Declined).ToString();
            Note = note;
        }

        public void MarkAsDeleteAttendanceRequest()
        {
            IsDeleted = true;
        }


    }
}

