using Attendance.Application.Attendance.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Attendance.Application.Attendance.Queries
{
    public class GetAttendanceQueryHandler : IRequestHandler<Queries.GetAttendance, AttendanceVM>
    {
        public GetAttendanceQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<AttendanceVM>
            Handle(Queries.GetAttendance request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetAttendance1ById ({request.Id})";

            var data = await _connection.QueryFirstAsync<AttendanceVM>
                (query);
            return data;
        }
    }
}

