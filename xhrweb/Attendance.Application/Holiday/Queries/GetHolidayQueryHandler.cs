using Attendance.Application.Holiday.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Attendance.Application.Holiday.Queries
{
    public class GetHolidayQueryHandler : IRequestHandler<Queries.GetHoliday, HolidayVM>
    {
        public GetHolidayQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<HolidayVM>
            Handle(Queries.GetHoliday request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetHolidayById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<HolidayVM>
                (query);
            return data;
        }
    }
}

