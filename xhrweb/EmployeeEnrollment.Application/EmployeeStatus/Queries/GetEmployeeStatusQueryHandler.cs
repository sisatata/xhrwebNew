using EmployeeEnrollment.Application.EmployeeStatus.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeStatus.Queries
{
    public class GetEmployeeStatusQueryHandler : IRequestHandler<Queries.GetEmployeeStatus, EmployeeStatusVM>
    {
        public GetEmployeeStatusQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeStatusVM>
            Handle(Queries.GetEmployeeStatus request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeStatusById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeStatusVM>
                (query);
            return data;
        }
    }
}

