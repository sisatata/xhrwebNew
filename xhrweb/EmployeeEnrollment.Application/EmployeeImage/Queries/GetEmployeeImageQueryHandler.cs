using EmployeeEnrollment.Application.EmployeeImage.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeImage.Queries
{
    public class GetEmployeeImageQueryHandler : IRequestHandler<Queries.GetEmployeeImage, EmployeeImageVM>
    {
        public GetEmployeeImageQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeImageVM>
            Handle(Queries.GetEmployeeImage request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from emloyee.GetEmployeeImageByEmployee ('{request.EmployeeId}')";

            var data = await _connection.QueryFirstAsync<EmployeeImageVM>
                (query);
            return data;
        }
    }
}

