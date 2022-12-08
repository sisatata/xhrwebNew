using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class EmployeeLeaveApplicationDataSpecificaion : BaseSpecification<LeaveApplication>

    {
        public EmployeeLeaveApplicationDataSpecificaion(Guid employeeId, string leaveCalendar)
            : base(x => x.EmployeeId == employeeId && x.LeaveCalendar == leaveCalendar && x.ApprovalStatus != "9")
        {
        }
    }
}
