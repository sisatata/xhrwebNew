using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeSalaryFilterSpecificaion : BaseSpecification<EmployeeSalary>

    {
        public EmployeeSalaryFilterSpecificaion(Guid employeeId)
            : base(x => x.EmployeeId == employeeId && x.IsDeleted == false)
        {
        }
    }
}
