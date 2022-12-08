using EmployeeEnrollment.Application.EmployeeDevice.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeDevice.Queries
{
    public class GetEmployeeDeviceQueryHandler : IRequestHandler<Queries.GetEmployeeDevice, EmployeeDeviceVM>
    {
        public GetEmployeeDeviceQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeDeviceVM>
            Handle(Queries.GetEmployeeDevice request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeDeviceById('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<EmployeeDeviceVM>
                (query);
            return data;
        }
    }
}

