using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public class GetAttendanceProcessedDataListByEmployeeAndDateRangeQueryHandler : IRequestHandler<Queries.GetAttendanceProcessedDataListByEmployeeAndDateRange, AttendanceDataVM>
    {
        public GetAttendanceProcessedDataListByEmployeeAndDateRangeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<AttendanceDataVM> Handle(Queries.GetAttendanceProcessedDataListByEmployeeAndDateRange request, CancellationToken cancellationToken)
        {
            AttendanceDataVM oModel = new AttendanceDataVM();
            var query = $"SELECT * from attendance.GetAttendanceProcessedDataListByEmployeeAndDateRange ('{request.EmployeeId}','{request.StartTime}','{request.EndTime}')";

            var data = await _connection.QueryAsync<AttendanceProcessedDataVM>(query);
            oModel.AttendanceProcessedDatas = data.ToList();

            var summary = $"SELECT * from  attendance.GetEmployeeAttendanceSummaryData('{request.EmployeeId}','{request.StartTime}','{request.EndTime}')";

            var summaryData = await _connection.QueryFirstOrDefaultAsync<EmployeeAttendanceSummaryVM>(summary);

            oModel.EmployeeAttendanceSummary = summaryData;

            return oModel;
        }
    }
}
