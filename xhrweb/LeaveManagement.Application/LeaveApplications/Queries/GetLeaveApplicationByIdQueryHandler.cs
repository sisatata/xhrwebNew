using Dapper;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveApplications.Queries
{
   public class GetLeaveApplicationByIdQueryHandler: IRequestHandler<Queries.GetLeaveApplicationById, LeaveApplicationVM>
    {

        public GetLeaveApplicationByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<LeaveApplicationVM> Handle(Queries.GetLeaveApplicationById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from leave.GetLeaveApplicationById('{request.LeaveApplicationId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<LeaveApplicationVM>(query);
            return data;
        }
    }
}
