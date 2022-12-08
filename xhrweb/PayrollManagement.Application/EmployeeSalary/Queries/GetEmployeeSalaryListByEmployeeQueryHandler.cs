using PayrollManagement.Application.EmployeeSalary.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;

namespace PayrollManagement.Application.EmployeeSalary.Queries
{
    public class GetEmployeeSalaryListByEmployeeQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryListByEmployee, List<EmployeeSalaryVM>>
    {
        public GetEmployeeSalaryListByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeSalaryVM>> Handle(Queries.GetEmployeeSalaryListByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeeSalaryByEmployee ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeSalaryVM>(query);
            foreach (var item in data.ToList())
            {
                var q = $"SELECT * from payroll.GetEmployeeSalaryComponentListByParent ('{item.Id}')";
                var d = await _connection.QueryAsync<EmployeeSalaryComponentVM>(q);
                item.EmployeeSalaryComponentList = d.ToList();
            }
            return data.ToList();
        }
    }
}
