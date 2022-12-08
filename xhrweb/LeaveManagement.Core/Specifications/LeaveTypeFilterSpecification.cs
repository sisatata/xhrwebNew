using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using System;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveTypeFilterSpecification : BaseSpecification<LeaveType>
    {
        public LeaveTypeFilterSpecification(Guid companyId)
            : base(x => x.CompanyId == companyId)
        {
        }
    }
}
