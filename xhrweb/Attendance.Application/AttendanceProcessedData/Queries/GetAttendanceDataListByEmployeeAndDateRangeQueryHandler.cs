using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public class GetAttendanceDataListByEmployeeAndDateRangeQueryHandler : IRequestHandler<Queries.GetAttendanceDataListByEmployeeAndDateRange, List<AttendanceProcessedDataVM>>
    {
        public GetAttendanceDataListByEmployeeAndDateRangeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceProcessedDataVM>> Handle(Queries.GetAttendanceDataListByEmployeeAndDateRange request, CancellationToken cancellationToken)
        {
            if (request.StartTime == null)
            {
                request.StartTime = DateTime.Now.AddMonths(-1);
            }
            if (request.EndTime == null)
            {
                request.EndTime = DateTime.Now;
            }
            var query = $"SELECT * from attendance.GetAttendanceProcessedDataListByEmployeeAndDateRange ('{request.EmployeeId}','{request.StartTime.Value.ToString("yyy-MM-dd")}','{request.EndTime.Value.ToString("yyy-MM-dd")}')"; ;

            var data = await _connection.QueryAsync<AttendanceProcessedDataVM>(query);
            return data.ToList();

            
        }
    }
}
