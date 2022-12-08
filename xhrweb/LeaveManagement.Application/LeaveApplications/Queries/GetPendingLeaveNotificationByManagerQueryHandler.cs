using Dapper;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveApplications.Queries
{
    public class GetPendingLeaveNotificationByManagerQueryHandler : IRequestHandler<Queries.GetPendingLeaveNotificationByManager, List<LeaveNotificationVM>>
    {
        public GetPendingLeaveNotificationByManagerQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<LeaveNotificationVM>> Handle(Queries.GetPendingLeaveNotificationByManager request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.StartDate.HasValue)
                {
                    request.StartDate = DateTime.Now.Date.AddDays(-30);
                }

                if (!request.EndDate.HasValue)
                {
                    request.EndDate = DateTime.Now;
                }
                var query = $"SELECT * from leave.GetPendingLeaveNotificationByManager('{request.ManagerId}','{request.StartDate.Value.ToString("yyyy-MM-dd")}', '{request.EndDate.Value.ToString("yyyy-MM-dd")}')";
                var data = await _connection.QueryAsync<LeaveNotificationVM>(query);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
