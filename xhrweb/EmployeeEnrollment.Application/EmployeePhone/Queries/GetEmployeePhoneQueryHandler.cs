using EmployeeEnrollment.Application.EmployeePhone.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeePhone.Queries
{
    public class GetEmployeePhoneQueryHandler : IRequestHandler<Queries.GetEmployeePhone, EmployeePhoneVM>
    {
        public GetEmployeePhoneQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeePhoneVM>
            Handle(Queries.GetEmployeePhone request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeePhoneById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeePhoneVM>
                (query);
            return data;
        }
    }
}

