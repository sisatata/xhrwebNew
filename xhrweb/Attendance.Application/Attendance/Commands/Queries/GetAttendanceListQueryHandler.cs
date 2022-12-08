using Attendance.Application.Attendance.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Attendance.Queries
{
    public class GetAttendanceListQueryHandler : IRequestHandler<Queries.GetAttendanceList, List<AttendanceVM>>
    {
        public GetAttendanceListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<AttendanceVM>> Handle(Queries.GetAttendanceList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetAttendance1ById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<AttendanceVM>(query);
            return data.ToList();
        }
    }
}
