using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeePaidIncomeTaxFilterSpecification : BaseSpecification<EmployeePaidIncomeTax>
    {
        public EmployeePaidIncomeTaxFilterSpecification(Guid companyId, Guid monthCycleId, Guid financialYearId)
            : base(x => x.CompanyId == companyId && x.MonthCycleId == monthCycleId && x.FinancialYearId == financialYearId)
        {

        }
    }
}