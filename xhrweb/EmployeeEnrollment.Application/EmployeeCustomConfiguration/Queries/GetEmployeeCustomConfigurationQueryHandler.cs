using EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries
{
    public class GetEmployeeCustomConfigurationQueryHandler : IRequestHandler<Queries.GetEmployeeCustomConfiguration, EmployeeCustomConfigurationVM>
    {
        public GetEmployeeCustomConfigurationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeCustomConfigurationVM>
            Handle(Queries.GetEmployeeCustomConfiguration request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeCustomConfigurationById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeCustomConfigurationVM>
                (query);
            return data;
        }
    }
}

