using MediatR;
using PayrollManagement.Application.Chart.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Chart.Queries
{
   public class PayrollChart : IRequest<ChartPayrollVM>
    {
        public Guid? CompanyId { get; set; }
        public Guid? MonthCycleId { get; set; }

        public string FinancialYearName { get; set; }

        public string MonthCycleName { get; set; }

    }
}
