using Dapper;
using LeaveManagement.Application.LeaveTypes.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveTypes.Queries
{
   public  class GetLeaveTypeByEmployeeQueryHandler : IRequestHandler<Queries.GetLeaveTypeByEmployee,List<LeaveTypeVM>>
    {
        public GetLeaveTypeByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<LeaveTypeVM>> Handle(Queries.GetLeaveTypeByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from leave.GetLeaveTypeByEmployee('{request.EmployeeId}')";
            var data = await _connection.QueryAsync<LeaveTypeVM>(query).ConfigureAwait(false);
            return data.ToList();
        }
    }
}
