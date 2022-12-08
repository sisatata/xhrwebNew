using PayrollManagement.Application.EmployeeSalary.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using PayrollManagement.Application.FinancialYear.Queries.Models;
using PayrollManagement.Application.MonthCycle.Queries.Models;

namespace PayrollManagement.Application.EmployeeSalary.Queries
{
    public class GetEmployeeSalaryHistoryByEmployeeQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryHistoryByEmployee, EmployeeSalaryHistoryVM>
    {
        public GetEmployeeSalaryHistoryByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeSalaryHistoryVM> Handle(Queries.GetEmployeeSalaryHistoryByEmployee request, CancellationToken cancellationToken)
        {
            EmployeeSalaryHistoryVM oModel = new EmployeeSalaryHistoryVM();

            var query = $"SELECT * from payroll. GetEmployeeSalaryByEmployee ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeSalaryVM>(query);
            foreach (var item in data.ToList())
            {
                var q = $"SELECT * from payroll.GetEmployeeSalaryComponentListByParent ('{item.Id}')";
                var d = await _connection.QueryAsync<EmployeeSalaryComponentVM>(q);
                item.EmployeeSalaryComponentList = d.ToList();
            }
            oModel.EmployeeSalaryList = data.ToList();

            var queryFin = $"SELECT * from payroll.GetFinancialYearByEmployeeId('{request.EmployeeId}')";

            var dataFin = await _connection.QueryAsync<FinancialYearVM>(queryFin);
            foreach (var item in dataFin.ToList())
            {
                var q = $"SELECT * from payroll.GetMonthCycleByEmployeeIdAndFinancialYear('{request.EmployeeId}','{item.Id}')";
                var d = await _connection.QueryAsync<MonthCycleVM>(q);
                item.MonthCycleList = d.ToList();
            }
            oModel.FinancialYearList = dataFin.ToList();
            return oModel;
        }
    }
}
