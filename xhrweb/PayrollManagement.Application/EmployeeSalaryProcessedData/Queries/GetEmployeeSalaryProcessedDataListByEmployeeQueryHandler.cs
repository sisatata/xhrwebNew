using PayrollManagement.Application.EmployeeSalaryProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Queries
{
    public class GetEmployeeSalaryProcessedDataListByEmployeeQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryProcessedDataListByEmployee, List<EmployeeSalaryProcessedDataVM>>
    {
        public GetEmployeeSalaryProcessedDataListByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeSalaryProcessedDataVM>> Handle(Queries.GetEmployeeSalaryProcessedDataListByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeeSalaryProcessedDataByEmployee ('{request.EmployeeId}','{request.FinancialYearId}','{request.MonthCycleId}')"; ;

            var data = await _connection.QueryAsync<EmployeeSalaryProcessedDataVM>(query);
            foreach (var item in data.ToList())
            {
                var q = $"SELECT * from payroll.GetEmployeeSalaryProcessedDataComponentByParent ('{item.Id}')";
                var d = await _connection.QueryAsync<EmployeeSalaryProcessedDataComponentVM>(q);
                item.EmployeeSalaryProcessedDataComponentList = d.ToList();
            }
            return data.ToList();
        }
    }
}
