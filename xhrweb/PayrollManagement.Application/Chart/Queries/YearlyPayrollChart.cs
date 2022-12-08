using MediatR;
using PayrollManagement.Application.Chart.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Chart.Queries
{
  public  class YearlyPayrollChart : IRequest<ChartPayrollVM>
    {
        public Guid? CompanyId { get; set; }
        public Guid? FinancialYearId { get; set; }
    }
}
