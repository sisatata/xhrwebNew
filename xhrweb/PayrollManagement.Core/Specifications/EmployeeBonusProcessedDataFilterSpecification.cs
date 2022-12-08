using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeBonusProcessedDataFilterSpecification : BaseSpecification<EmployeeBonusProcessedData>

    {
        public EmployeeBonusProcessedDataFilterSpecification(Guid companyId, Guid financialYearId, Guid bonusTypeId)
            : base(x => x.CompanyId == companyId && x.FinancialYearId == financialYearId && x.BonusTypeId == bonusTypeId)
        {
        }
    }
}