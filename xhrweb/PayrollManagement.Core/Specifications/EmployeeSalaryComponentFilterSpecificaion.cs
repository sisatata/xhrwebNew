using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeSalaryComponentFilterSpecificaion : BaseSpecification<EmployeeSalaryComponent>

    {
        public EmployeeSalaryComponentFilterSpecificaion(Guid companyId, Guid? EmployeeSalaryId)
            : base(x => x.CompanyId == companyId && x.IsDeleted == false && (EmployeeSalaryId == Guid.Empty || x.EmployeeSalaryId == EmployeeSalaryId))
        {
        }
    }
}
