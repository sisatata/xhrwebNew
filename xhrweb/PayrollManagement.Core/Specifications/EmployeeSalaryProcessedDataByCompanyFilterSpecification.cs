using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeSalaryProcessedDataByCompanyFilterSpecification : BaseSpecification<EmployeeSalaryProcessedData>

    {
        public EmployeeSalaryProcessedDataByCompanyFilterSpecification(Guid companyId, Guid financialYearId, Guid monthCycleId)
            : base(x => x.CompanyId == companyId && x.FinancialYearId == financialYearId && x.MonthCycleId == monthCycleId && x.IsDeleted == false)
        {
        }
    }
}
