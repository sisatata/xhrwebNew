using CompanySetup.Application.MonthCycle.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.MonthCycle.Queries
{
   public static class Queries
    {
        public class GetMonthCycleByCompanyIdandFinancialYear : IRequest<List<MonthCycleVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid FinancialYearId { get; set; }
        }

        public class GetMonthCycleById : IRequest<MonthCycleVM>
        {
            public Guid Id { get; set; }
        }
    }
}
