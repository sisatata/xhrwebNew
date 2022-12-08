using EmployeeEnrollment.Application.EmployeeEmail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeEmail.Queries
{
    public class GetEmployeeEmailQueryHandler : IRequestHandler<Queries.GetEmployeeEmailByID, EmployeeEmailVM>
    {
        public GetEmployeeEmailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeEmailVM>
            Handle(Queries.GetEmployeeEmailByID request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeEmailById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeEmailVM>
                (query);
            return data;
        }
    }
}

