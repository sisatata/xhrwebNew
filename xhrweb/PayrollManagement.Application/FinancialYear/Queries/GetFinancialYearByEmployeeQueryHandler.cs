using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PayrollManagement.Application.FinancialYear.Queries.Models;
using Dapper;
using MediatR;
using PayrollManagement.Application.MonthCycle.Queries.Models;

namespace PayrollManagement.Application.FinancialYear.Queries
{
    public class GetFinancialYearByEmployeeQueryHandler : IRequestHandler<Queries.GetFinancialYearByEmployeeId, List<FinancialYearVM>>
    {
        public GetFinancialYearByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<FinancialYearVM>> Handle(Queries.GetFinancialYearByEmployeeId request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetFinancialYearByEmployeeId('{request.EmployeeId}')";

            var data = await _connection.QueryAsync<FinancialYearVM>(query);
            foreach (var item in data.ToList())
            {
                var q = $"SELECT * from payroll.GetMonthCycleByEmployeeIdAndFinancialYear('{request.EmployeeId}','{item.Id}')";
                var d = await _connection.QueryAsync<MonthCycleVM>(q);
                item.MonthCycleList = d.ToList();
            }
            return data.ToList();

        }
    }
}
