using PayrollManagement.Application.EmployeeLeaveEncashment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Queries
{
    public class GetEmployeeLeaveEncashmentQueryHandler : IRequestHandler<Queries.GetEmployeeLeaveEncashment, EmployeeLeaveEncashmentVM>
    {
        public GetEmployeeLeaveEncashmentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeLeaveEncashmentVM>
            Handle(Queries.GetEmployeeLeaveEncashment request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeLeaveEncashmentById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeLeaveEncashmentVM>
                (query);
            return data;
        }
    }
}

