using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class FinancialYearMonthByCompanyLastMonthFilterSpecificaion : BaseSpecification<FinancialYearMonth>

    {
        public FinancialYearMonthByCompanyLastMonthFilterSpecificaion(Guid companyId, int lastMonthNumber, int financialNumber)
            : base(x => x.CompanyId == companyId && x.MonthNumber == lastMonthNumber && x.YearNumber == financialNumber)
        {
        }
    }
}
