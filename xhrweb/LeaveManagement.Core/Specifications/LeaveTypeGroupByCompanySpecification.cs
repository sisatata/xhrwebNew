using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using System;

namespace LeaveManagement.Core.Specifications
{
    public class LeaveTypeGroupByCompanySpecification : BaseSpecification<LeaveTypeGroup>
    {
        public LeaveTypeGroupByCompanySpecification(Guid companyId)
            : base(x => x.CompanyId == companyId)
        {
        }
    }
}
