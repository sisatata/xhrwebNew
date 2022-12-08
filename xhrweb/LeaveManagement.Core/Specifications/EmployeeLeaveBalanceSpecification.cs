using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class EmployeeLeaveBalanceSpecification : BaseSpecification<LeaveBalance>
    {
        public EmployeeLeaveBalanceSpecification(Guid companyId, bool isDeleted)
           : base(x => x.CompanyId == companyId && x.IsDeleted == false)
        {
        }
    }
}
