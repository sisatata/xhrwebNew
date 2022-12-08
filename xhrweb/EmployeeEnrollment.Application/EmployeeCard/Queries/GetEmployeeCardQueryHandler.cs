using EmployeeEnrollment.Application.EmployeeCard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeCard.Queries
{
    public class GetEmployeeCardQueryHandler : IRequestHandler<Queries.GetEmployeeCard, EmployeeCardVM>
    {
        public GetEmployeeCardQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeCardVM>
            Handle(Queries.GetEmployeeCard request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeCardById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeCardVM>
                (query);
            return data;
        }
    }
}

