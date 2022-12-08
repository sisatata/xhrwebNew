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
    public class GetEmployeeDeviceByEmployeeQueryHandler : IRequestHandler<Queries.GetEmployeeDeviceByEmployee, EmployeeDeviceDetailVM>
    {
        public GetEmployeeDeviceByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeDeviceDetailVM>
            Handle(Queries.GetEmployeeDeviceByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeDeviceById('{request.EmployeeId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<EmployeeDeviceDetailVM>
                (query);
            return data;
        }
    }
}

