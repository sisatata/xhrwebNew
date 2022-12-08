using Attendance.Application.Shift.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Shift.Queries
{
   public class GetShiftByCompanyQueryHandler: IRequestHandler<Queries.GetShiftByCompany, List<ShiftVM>>
    {
        private readonly DbConnection _connection;

        public GetShiftByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ShiftVM>> Handle(Queries.GetShiftByCompany request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from attendance.GetShiftByCompany('{request.CompanyId}')";

                var data = await _connection.QueryAsync<ShiftVM>(query);

                return data.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
