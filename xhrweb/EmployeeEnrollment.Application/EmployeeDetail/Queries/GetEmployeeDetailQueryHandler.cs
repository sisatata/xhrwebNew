using EmployeeEnrollment.Application.EmployeeDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeDetail.Queries
{
    public class GetEmployeeDetailByEmployeeQueryHandler : IRequestHandler<Queries.GetEmployeeDetailByEmployee, EmployeeDetailVM>
    {
        public GetEmployeeDetailByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeDetailVM> Handle(Queries.GetEmployeeDetailByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeDetailByEmployeeId ('{request.EmployeeId}')";

                var data = await _connection.QueryFirstOrDefaultAsync<EmployeeDetailVM>(query); 
                return data;
        }
    }
}

