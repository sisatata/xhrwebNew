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
    public class GetShiftGroupByCompanyQueryHandler : IRequestHandler<Queries.GetShiftGroupByCompany, List<ShiftGroupVM>>
    {
        private readonly DbConnection _connection;

        public GetShiftGroupByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ShiftGroupVM>> Handle(Queries.GetShiftGroupByCompany request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from attendance.GetShiftGroupByCompany('{request.CompanyId}')";

                var data = await _connection.QueryAsync<ShiftGroupVM>(query);

                return data.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
