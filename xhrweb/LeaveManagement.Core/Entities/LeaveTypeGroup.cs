using ASL.Hrms.SharedKernel;
using LeaveManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace LeaveManagement.Core.Entities
{
    public class LeaveTypeGroup : BaseEntity<int>, IAggregateRoot
    {
        public string LeaveTypeGroupName { get; private set; }
        public string LeaveTypeGroupNameLC { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public LeaveTypeGroup(int id) : base(id) { }
        private LeaveTypeGroup() : base(0) { }

        public static LeaveTypeGroup Create(

         string leaveTypeGroupName,
         string leaveTypeGroupNameLC,
         Guid? companyId,
         Boolean isDeleted

        )

        {
            var oModel = new LeaveTypeGroup();

            oModel.LeaveTypeGroupName = leaveTypeGroupName;
            oModel.LeaveTypeGroupNameLC = leaveTypeGroupNameLC;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }
        public void UpdateLeaveTypeGroup
            (
         string leaveTypeGroupName,
         string leaveTypeGroupNameLC,
         Guid? companyId,
         Boolean isDeleted
        )
        {
            LeaveTypeGroupName = leaveTypeGroupName;
            LeaveTypeGroupNameLC = leaveTypeGroupNameLC;
            CompanyId = companyId;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteLeaveTypeGroup()
        {
            IsDeleted = true;
        }


    }
}

