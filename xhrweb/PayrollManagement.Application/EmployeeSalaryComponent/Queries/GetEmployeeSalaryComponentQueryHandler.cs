using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Queries
{
    public class GetEmployeeSalaryComponentQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryComponent, EmployeeSalaryComponentVM>
    {
        public GetEmployeeSalaryComponentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeSalaryComponentVM>
            Handle(Queries.GetEmployeeSalaryComponent request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeSalaryComponentById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeSalaryComponentVM>
                (query);
            return data;
        }
    }
}

