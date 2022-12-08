using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class FinancialYearMonthByCompanyLastYearFilterSpecificaion : BaseSpecification<FinancialYearMonth>

    {
        public FinancialYearMonthByCompanyLastYearFilterSpecificaion(Guid companyId, int lastYearNumber)
            : base(x => x.CompanyId == companyId && x.YearNumber == lastYearNumber)
        {
        }
    }
}
