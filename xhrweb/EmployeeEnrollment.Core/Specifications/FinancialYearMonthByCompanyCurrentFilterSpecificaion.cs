using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class FinancialYearMonthByCompanyCurrentFilterSpecificaion : BaseSpecification<FinancialYearMonth>

    {
        public FinancialYearMonthByCompanyCurrentFilterSpecificaion(Guid companyId)
            : base(x => x.CompanyId == companyId)
        {
        }
    }
}
