using PayrollManagement.Application.EmployeeSalary.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeSalary.Queries
{
    public class GetEmployeeSalaryQueryHandler : IRequestHandler<Queries.GetEmployeeSalary, EmployeeSalaryVM>
    {
        public GetEmployeeSalaryQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeSalaryVM>
            Handle(Queries.GetEmployeeSalary request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeSalaryById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeSalaryVM>
                (query);
            return data;
        }
    }
}

