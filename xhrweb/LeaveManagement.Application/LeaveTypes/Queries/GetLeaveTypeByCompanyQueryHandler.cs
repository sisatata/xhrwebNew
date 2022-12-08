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
    public class GetLeaveTypeByCompanyQueryHandler : IRequestHandler<Queries.GetLeaveTypeByCompany, List<LeaveTypeVM>>
    {
        public GetLeaveTypeByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<LeaveTypeVM>> Handle(Queries.GetLeaveTypeByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from leave.GetLeaveTypeByCompany('{request.CompanyId}')";
            var data = await _connection.QueryAsync<LeaveTypeVM>(query).ConfigureAwait(false);
            return data.OrderBy(x => x.LeaveTypeGroupName).ToList();
        }
    }
}
