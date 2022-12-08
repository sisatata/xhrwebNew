using PayrollManagement.Application.MonthCycle.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.MonthCycle.Queries
{
   public static class Queries
    {
        public class GetMonthCycleByEmployeeIdAndFinancialYear : IRequest<List<MonthCycleVM>>
        {
            public Guid EmployeeId { get; set; }
            public Guid FinancialYearId { get; set; }
        }
    }
}
