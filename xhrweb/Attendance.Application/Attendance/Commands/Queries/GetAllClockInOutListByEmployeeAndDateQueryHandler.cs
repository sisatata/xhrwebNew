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
    public class GetAllClockInOutListByEmployeeAndDateQueryHandler : IRequestHandler<Queries.GetAllClockInOutListByEmployeeAndDate, List<ClockInOutVM>>
    {
        public GetAllClockInOutListByEmployeeAndDateQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ClockInOutVM>> Handle(Queries.GetAllClockInOutListByEmployeeAndDate request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetAllClockInOutByEmployeeAndDate('{request.EmployeeId}','{request.RequestDate.ToString("yyyy-MM-dd")}')"; ;

            var data = await _connection.QueryAsync<ClockInOutVM>(query);
            return data.ToList();
        }
    }
}
