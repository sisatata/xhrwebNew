using Dapper;
using LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary
{
    public class GetLeaveBalanceSummaryByEmployeeQueryHandler : IRequestHandler<LeaveBalanceSummary.GetLeaveBalanceSummaryByEmployee, List<LeaveBalanceSummaryVM>>
    {
        public GetLeaveBalanceSummaryByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;
        public async Task<List<LeaveBalanceSummaryVM>> Handle(LeaveBalanceSummary.GetLeaveBalanceSummaryByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from leave.GetLeaveBalanceSummaryByEmployee('{request.EmployeeId }','{request.LeaveCalendar}')";
            var data = await _connection.QueryAsync<LeaveBalanceSummaryVM>(query).ConfigureAwait(false);
            return data.ToList();
        }
    }
}
