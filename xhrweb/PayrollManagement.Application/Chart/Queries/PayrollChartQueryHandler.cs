using CompanySetup.Application.FinancialYear.Queries.Models;
using Dapper;
using MediatR;
using PayrollManagement.Application.BankBranch.Queries.Models;
using PayrollManagement.Application.Chart.Model;
using PayrollManagement.Application.Chart.Queries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Chart.Queries
{
    public class PayrollChartQueryHandler : IRequestHandler<PayrollChart, ChartPayrollVM>
    {
        #region ctor
        public PayrollChartQueryHandler(DbConnection connection)
        {
            _connection = connection;

        }
        #endregion
        #region propeties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<ChartPayrollVM> Handle(PayrollChart request, CancellationToken cancellationToken)
        {
            try
            {
                var query = "";
                /*var financialYearIdQuery = $"SELECT * from main.GetFinancialYearidByYearName('{request.CompanyId}','{request.FinancialYearName}')";
                var financialYearId = await _connection.QueryAsync<FinancialYearIdAndNameVM>(financialYearIdQuery);
                var financialYear = financialYearId.FirstOrDefault().FinancialYearId;
                var monthCycleIdQuery = $"SELECT * from main.GetMonthCycleidByMonthCycleName('{request.CompanyId}','{financialYear}','{request.MonthCycleName}')";
                var monthCycle = await _connection.QueryAsync<MonthCycleIdAndNameVM>(monthCycleIdQuery);*/
                query = $"SELECT * from payroll.ChartGetLastMonthPayroll('{request.CompanyId}','{request.FinancialYearName}','{request.MonthCycleName}')";
                var data = await _connection.QueryAsync<ChartPayrollVM>(query);
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }
        #endregion
    }
}
