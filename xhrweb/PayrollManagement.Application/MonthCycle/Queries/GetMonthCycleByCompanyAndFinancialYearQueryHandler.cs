using PayrollManagement.Application.MonthCycle.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.MonthCycle.Queries
{
  public  class GetMonthCycleByCompanyAndFinancialYearQueryHandler: IRequestHandler<Queries.GetMonthCycleByEmployeeIdAndFinancialYear, List<MonthCycleVM>>
    {
        public GetMonthCycleByCompanyAndFinancialYearQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<MonthCycleVM>> Handle(Queries.GetMonthCycleByEmployeeIdAndFinancialYear request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetMonthCycleByCompanyIdandFinancialYear('{request.EmployeeId}','{request.FinancialYearId}')";

            var data = await _connection.QueryAsync<MonthCycleVM>(query);
            return data.ToList();
        }
    }
}
