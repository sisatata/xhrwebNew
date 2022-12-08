using Attendance.Application.AttendanceRequest.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Attendance.Application.AttendanceRequest.Queries
{
    public class GetAttendanceRequestQueryHandler : IRequestHandler<Queries.GetAttendanceRequest, AttendanceRequestVM>
    {
        public GetAttendanceRequestQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<AttendanceRequestVM>
            Handle(Queries.GetAttendanceRequest request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetAttendanceRequestById('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<AttendanceRequestVM> (query);
            return data;
        }
    }
}

