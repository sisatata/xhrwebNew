using Attendance.Application.Holiday.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.Holiday.Queries
{
    public class GetHolidayListByFinancialYearQueryHandler : IRequestHandler<Queries.GetHolidayListByFinancialYear, List<HolidayVM>>
    {
        public GetHolidayListByFinancialYearQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<List<HolidayVM>> Handle(Queries.GetHolidayListByFinancialYear request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from attendance.GetHolidayByFinancialYear('{request.CompanyId}', '{request.Financialyear}')"; 
                var data = await _connection.QueryAsync<HolidayVM>(query);
                return data.ToList();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }
    }
}
