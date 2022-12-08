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
    public class GetEmployeeSalaryListByCompanyQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryListByCompany, List<EmployeeSalaryVM>>
    {
        public GetEmployeeSalaryListByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeSalaryVM>> Handle(Queries.GetEmployeeSalaryListByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeeCurrentSalaryByCompany ('{request.CompanyId}')";

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
