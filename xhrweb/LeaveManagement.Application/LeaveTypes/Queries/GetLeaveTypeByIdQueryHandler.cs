using Dapper;
using LeaveManagement.Application.LeaveTypes.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveTypes.Queries
{
   public class GetLeaveTypeByIdQueryHandler: IRequestHandler<Queries.GetLeaveTypeById, LeaveTypeVM>
    {
        public GetLeaveTypeByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;
        public async Task<LeaveTypeVM> Handle(Queries.GetLeaveTypeById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetLeaveTypeById('{request.LeaveTypeId}')";
            var data = await _connection.QueryFirstOrDefaultAsync<LeaveTypeVM>(query);
            return data;
        }
    }
}
