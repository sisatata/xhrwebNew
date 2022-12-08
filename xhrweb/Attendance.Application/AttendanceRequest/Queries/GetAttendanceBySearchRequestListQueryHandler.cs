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
    public class GetAttendanceBySearchRequestListQueryHandler : IRequestHandler<Queries.GetAttendanceBySearchRequestList, List<AttendanceRequestVM>>
    {
        public GetAttendanceBySearchRequestListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceRequestVM>> Handle(Queries.GetAttendanceBySearchRequestList request, CancellationToken cancellationToken)
        {
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from attendance.GetAttendanceRequestBySearch('{request.EmployeeId}',{request.RequestTypeId},'{startDate}','{endDate}')"; 
            var data = await _connection.QueryAsync<AttendanceRequestVM>(query);
            return data.ToList();
        }
    }
}
