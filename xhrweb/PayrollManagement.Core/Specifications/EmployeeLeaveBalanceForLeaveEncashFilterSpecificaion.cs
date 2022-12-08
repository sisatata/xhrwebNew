using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeLeaveBalanceForLeaveEncashFilterSpecificaion : BaseSpecification<LeaveBalance>

    {
        public EmployeeLeaveBalanceForLeaveEncashFilterSpecificaion(Guid companyId, Guid? employeeId, DateTime leaveEncashDate)
            : base(x => x.CompanyId == companyId && x.Id == employeeId)
        {
        }
    }
}
