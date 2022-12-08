using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeEnrollmentForLeaveEncashFilterSpecificaion : BaseSpecification<Employee>

    {
        public EmployeeEnrollmentForLeaveEncashFilterSpecificaion(Guid companyId, Guid? employeeId, DateTime leaveEncashDate)
            : base(x => x.CompanyId == companyId && x.JoiningDate < leaveEncashDate.AddYears(-1)
            && (employeeId == null || employeeId == Guid.Empty || x.Id == employeeId))
        {
        }
    }
}
