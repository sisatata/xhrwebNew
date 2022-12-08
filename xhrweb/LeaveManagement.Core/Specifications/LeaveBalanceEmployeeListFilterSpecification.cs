using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using System;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveBalanceEmployeeListFilterSpecification : BaseSpecification<LeaveBalance>
    {
        public LeaveBalanceEmployeeListFilterSpecification(Guid leaveTypeId, string leaveCalendar)
            : base(x => x.LeaveTypeId == leaveTypeId && x.LeaveCalendar == leaveCalendar)
        {
        }
    }
}
