using Attendance.Application.Shift.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Shift.Queries
{
   public  class GetShiftByIdQueryHandler: IRequestHandler<Queries.GetShiftById, ShiftVM>
    {
        private readonly DbConnection _connection;

        public GetShiftByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ShiftVM> Handle(Queries.GetShiftById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from attendance.GetShiftById('{request.ShiftId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<ShiftVM>(query);

            return data;
        }
    }
}
