using Attendance.Application.AttendanceRequest.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceRequest.Queries
{
    public class GetPendingAttendanceReqNotificationByManagerQueryHandler : IRequestHandler<Queries.GetPendingAttendanceReqNotificationByManager, List<AttendanceRequestNotificationVM>>
    {
        public GetPendingAttendanceReqNotificationByManagerQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceRequestNotificationVM>> Handle(Queries.GetPendingAttendanceReqNotificationByManager request, CancellationToken cancellationToken)
        {
            if (!request.StartTime.HasValue)
            {
                request.StartTime = DateTime.Now.Date.AddDays(-30);
            }

            if (!request.EndTime.HasValue)
            {
                request.EndTime = DateTime.Now;
            }
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from attendance.GetAttendanceRequestApprovePendingQueue('{request.ManagerId}','{startDate}', '{endDate}')";
            var data = await _connection.QueryAsync<AttendanceRequestNotificationVM>(query);
            return data.ToList();
        }
    }
}
