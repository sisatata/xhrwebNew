using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeSalaryByCompanyFilterSpecificaion : BaseSpecification<EmployeeSalary>

    {
        public EmployeeSalaryByCompanyFilterSpecificaion(Guid companyId, Guid employeeId, DateTime changeDate)
            : base(x => x.CompanyId == companyId && x.EmployeeId == employeeId
            && x.IsDeleted == false && changeDate >= x.StartDate && changeDate <= x.EndDate)
        {
        }
    }
}
