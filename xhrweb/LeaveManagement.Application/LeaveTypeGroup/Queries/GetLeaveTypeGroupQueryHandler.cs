using LeaveManagement.Application.LeaveTypeGroup.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace LeaveManagement.Application.LeaveTypeGroup.Queries
{
    public class GetLeaveTypeGroupQueryHandler : IRequestHandler<Queries.GetLeaveTypeGroup, LeaveTypeGroupVM>
    {
        public GetLeaveTypeGroupQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<LeaveTypeGroupVM>
            Handle(Queries.GetLeaveTypeGroup request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetLeaveTypeGroupById ({request.Id})";

            var data = await _connection.QueryFirstAsync<LeaveTypeGroupVM> (query);
            return data;
        }
    }
}

