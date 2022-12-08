using EmployeeEnrollment.Application.EmployeeStatusHistory.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Queries
{
    public class GetEmployeeStatusHistoryQueryHandler : IRequestHandler<Queries.GetEmployeeStatusHistory, EmployeeStatusHistoryVM>
    {
        public GetEmployeeStatusHistoryQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeStatusHistoryVM>
            Handle(Queries.GetEmployeeStatusHistory request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeStatusHistoryById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeStatusHistoryVM>
                (query);
            return data;
        }
    }
}

