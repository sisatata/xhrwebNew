using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveApplicationApproveQueueFilterSpecification : BaseSpecification<LeaveApplicationApproveQueue>
    {
        public LeaveApplicationApproveQueueFilterSpecification(Guid leaveApplicationId, Guid managerId)
            : base(x => x.LeaveApplicationId == leaveApplicationId && x.ManagerId == managerId)
        {
        }
    }
}
