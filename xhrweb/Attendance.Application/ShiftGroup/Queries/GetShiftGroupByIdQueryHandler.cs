using Attendance.Application.ShiftGroup.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftGroup.Queries
{
    public class GetShiftGroupByIdQueryHandler : IRequestHandler<Queries.GetShiftGroupById, ShiftGroupVM>
    {
        private readonly DbConnection _connection;

        public GetShiftGroupByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ShiftGroupVM> Handle(Queries.GetShiftGroupById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetShiftGroupById('{request.ShiftGroupId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<ShiftGroupVM>(query);

            return data;
        }
    }
}