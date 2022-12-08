using LeaveManagement.Application.LeaveTypeGroup.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveTypeGroup.Queries
{
    public class GetLeaveTypeGroupListQueryHandler : IRequestHandler<Queries.GetLeaveTypeGroupList, List<LeaveTypeGroupVM>>
    {
        public GetLeaveTypeGroupListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<LeaveTypeGroupVM>> Handle(Queries.GetLeaveTypeGroupList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from Leave.GetLeaveTypeGroupByCompanyId ('{request.CompanyId}')";
            var data = await _connection.QueryAsync<LeaveTypeGroupVM>(query);
            return data.ToList();
        }
    }
}
