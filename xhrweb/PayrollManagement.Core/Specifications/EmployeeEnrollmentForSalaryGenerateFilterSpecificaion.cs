using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeEnrollmentForSalaryGenerateFilterSpecificaion : BaseSpecification<Employee>

    {
        public EmployeeEnrollmentForSalaryGenerateFilterSpecificaion(Guid companyId, Guid? employeeId, DateTime salaryMonthStartDate, DateTime salaryMonthEndDate)
            : base(x => x.CompanyId == companyId && x.JoiningDate < salaryMonthEndDate &&
            (x.QuitDate == null || x.QuitDate > salaryMonthStartDate.Date) && (employeeId == null || employeeId == Guid.Empty || x.Id == employeeId))
        {
        }
    }
}
