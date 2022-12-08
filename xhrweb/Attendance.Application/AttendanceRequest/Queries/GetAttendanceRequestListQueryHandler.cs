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
    public class GetAttendanceRequestListQueryHandler : IRequestHandler<Queries.GetAttendanceRequestList, List<AttendanceRequestVM>>
    {
        public GetAttendanceRequestListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceRequestVM>> Handle(Queries.GetAttendanceRequestList request, CancellationToken cancellationToken)
        {
            if (!request.StartTime.HasValue)
            {
                request.StartTime = DateTime.Now.Date.AddDays(-30);
            }

            if (!request.EndTime.HasValue)
            {
                request.EndTime = DateTime.Now;
            }

            var query = $"SELECT * from attendance.GetAttendanceRequestByCompany('{request.CompanyId}','{request.StartTime}', '{request.EndTime}')";

            var data = await _connection.QueryAsync<AttendanceRequestVM>(query);
            return data.ToList();
        }
    }
}
