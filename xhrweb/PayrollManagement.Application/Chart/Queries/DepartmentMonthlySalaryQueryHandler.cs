using CompanySetup.Application.FinancialYear.Queries.Models;
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
    public class DepartmentMonthlySalaryQueryHandler : IRequestHandler<Queries.DepartmentMonthlySalary, List<DepartmentSalaryVM>>
    {
        #region ctor
        public DepartmentMonthlySalaryQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<List<DepartmentSalaryVM>> Handle(Queries.DepartmentMonthlySalary request, CancellationToken cancellationToken)
        {
          
          /*  var financialYearIdQuery = $"SELECT * from main.GetFinancialYearidByYearName('{request.CompanyId}','{request.FinancialYearName}')";
            var financialYearId = await _connection.QueryAsync<FinancialYearIdAndNameVM>(financialYearIdQuery);
            var financialYear = financialYearId.FirstOrDefault().FinancialYearId;
            var monthCycleIdQuery = $"SELECT * from main.GetMonthCycleidByMonthCycleName('{request.CompanyId}','{financialYear}','{request.MonthCycleName}')";
            var monthCycle = await _connection.QueryAsync<MonthCycleIdAndNameVM>(monthCycleIdQuery);*/
            var query = $"SELECT * from payroll.chartgetlastmonthdepartmentwisesalary('{request.CompanyId}','{request.FinancialYearName}','{request.MonthCycleName}')";
            var data = await _connection.QueryAsync<DepartmentSalaryVM>(query);
            return data.ToList();
            
        }
        #endregion
    }
}
