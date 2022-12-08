using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using System;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveBalanceByTypeSpecification : BaseSpecification<LeaveBalance>
    {
        public LeaveBalanceByTypeSpecification(Guid leaveTypeId, string leaveCalendar, Guid employeeId, bool isDeleted)
            : base(x => x.LeaveTypeId == leaveTypeId && x.LeaveCalendar == leaveCalendar && x.EmployeeId == employeeId)
        {
        }
    }
}
