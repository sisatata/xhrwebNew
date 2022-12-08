using Attendance.Application.Holiday.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Holiday.Queries
{
    public class GetHolidayListQueryHandler : IRequestHandler<Queries.GetHolidayList, List<HolidayVM>>
    {
        public GetHolidayListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<HolidayVM>> Handle(Queries.GetHolidayList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetHolidayByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<HolidayVM>(query);
            return data.ToList();
        }
    }
}
