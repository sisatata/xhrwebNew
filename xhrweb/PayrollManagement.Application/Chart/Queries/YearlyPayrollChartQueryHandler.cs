using Dapper;
using MediatR;
using PayrollManagement.Application.Chart.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Chart.Queries
{
    public class YearlyPayrollChartQueryHandler : IRequestHandler<YearlyPayrollChart, ChartPayrollVM>
    {
        public YearlyPayrollChartQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private DbConnection _connection;
        public async Task<ChartPayrollVM> Handle(YearlyPayrollChart request, CancellationToken cancellationToken)
        {
            var query = "";
            query = $"SELECT * from payroll.ChartGetYearlyPayroll('{request.CompanyId}','{request.FinancialYearId}')";
            var data = await _connection.QueryAsync<ChartPayrollVM>(query);
            return data.FirstOrDefault();

        }
    }
}
