using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Queries
{
    public class GetEmployeeSalaryComponentListByParentQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryComponentListByParent, List<EmployeeSalaryComponentVM>>
    {
        public GetEmployeeSalaryComponentListByParentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeSalaryComponentVM>> Handle(Queries.GetEmployeeSalaryComponentListByParent request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetEmployeeSalaryComponentListByParent ('{request.EmployeeSalaryId}')";

            var data = await _connection.QueryAsync<EmployeeSalaryComponentVM>(query);

            

            return data.ToList();
        }
    }
}
