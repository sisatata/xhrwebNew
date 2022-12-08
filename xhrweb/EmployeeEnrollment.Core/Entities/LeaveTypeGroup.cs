using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class LeaveTypeGroup : BaseEntity<int>
    {
        public string LeaveTypeGroupName { get; private set; }
        public string LeaveTypeGroupNameLC { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public LeaveTypeGroup(int id) : base(id) { }
        private LeaveTypeGroup() : base(0) { }
    }
}

