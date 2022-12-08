using CompanySetup.Application.Chart.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Chart.Queries
{
    public static class CompanyChartQueries
    {
        public  class GetCurrentYearJoinedEmployeesByCurrentYear : IRequest<List<CurrentYearJoinedEmployeesVM>>
        {
            public Guid CompanyId { get; set; }
            public string CurrentYear { get; set; }
        }
    }
}
