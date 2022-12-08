using CompanySetup.Application.Chart.Model;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Chart.Queries
{
    public class GetCurrentYearJoinedEmployeesQueryHandler : IRequestHandler<CompanyChartQueries.GetCurrentYearJoinedEmployeesByCurrentYear, List<CurrentYearJoinedEmployeesVM>>
    {
        public GetCurrentYearJoinedEmployeesQueryHandler(DbConnection connection)
        {
            _connection = connection;

        }
        private readonly DbConnection _connection;

        public async Task<List<CurrentYearJoinedEmployeesVM>> Handle(CompanyChartQueries.GetCurrentYearJoinedEmployeesByCurrentYear request, CancellationToken cancellationToken)
        {
            var query = "";
            query = $"SELECT * from main.ChartGetCurrentYearJoinedEmployees('{request.CompanyId}','{request.CurrentYear}')";
            var data = await _connection.QueryAsync<CurrentYearJoinedEmployeesVM>(query);
            return data.ToList();
        }

        
    }
}
