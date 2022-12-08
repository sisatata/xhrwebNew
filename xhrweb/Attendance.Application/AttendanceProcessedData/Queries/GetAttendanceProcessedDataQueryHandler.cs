using Attendance.Application.AttendanceProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public class GetAttendanceProcessedDataQueryHandler : IRequestHandler<Queries.GetAttendanceProcessedData, AttendanceProcessedDataVM>
    {
        public GetAttendanceProcessedDataQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<AttendanceProcessedDataVM>
            Handle(Queries.GetAttendanceProcessedData request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetAttendanceProcessedDataById ({request.Id})";

            var data = await _connection.QueryFirstAsync<AttendanceProcessedDataVM>
                (query);
            return data;
        }
    }
}

