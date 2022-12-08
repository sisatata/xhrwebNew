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
    public class GetAttendanceProcessedDataListQueryHandler : IRequestHandler<Queries.GetAttendanceProcessedDataList, List<AttendanceProcessedDataVM>>
    {
        public GetAttendanceProcessedDataListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceProcessedDataVM>> Handle(Queries.GetAttendanceProcessedDataList request, CancellationToken cancellationToken)
        {
            if(request.StartTime == null)
            {
                request.StartTime = DateTime.Now.AddMonths(-1);
            }
            if(request.EndTime == null)
            {
                request.EndTime = DateTime.Now;
            }

            var query = $"SELECT * from attendance.getattendanceprocesseddatalistbycompanyanddaterange('{request.CompanyId}','{request.StartTime.Value.ToString("yyyy-MM-dd")}','{request.EndTime.Value.ToString("yyyy-MM-dd")}')"; 

            var data = await _connection.QueryAsync<AttendanceProcessedDataVM>(query);
            return data.ToList();
        }
    }
}
