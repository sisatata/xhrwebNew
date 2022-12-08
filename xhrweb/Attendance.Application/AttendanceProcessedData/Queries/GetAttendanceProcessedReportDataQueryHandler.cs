using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public class GetAttendanceProcessedReportDataQueryHandler : IRequestHandler<Queries.GetAttendanceProcessedReportData, List<ReportAttendanceProcessedDataVM>>
    {
        public GetAttendanceProcessedReportDataQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ReportAttendanceProcessedDataVM>> Handle(Queries.GetAttendanceProcessedReportData request, CancellationToken cancellationToken)
        {
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");

            var query = "";
            if (request.EmployeeId == Guid.Empty || request.EmployeeId==null)
            {
                query = $"SELECT * from attendance.RPTGetAttendanceProcessedDataListByCompanyAndDateRange('{request.CompanyId}',null,'{startDate}','{endDate}')"; ;
            }
            else
            {
                query = $"SELECT * from attendance.RPTGetAttendanceProcessedDataListByCompanyAndDateRange('{request.CompanyId}','{request.EmployeeId}','{startDate}','{endDate}')"; ;

            }
            //var query = $"SELECT * from attendance.RPTGetAttendanceProcessedDataListByCompanyAndDateRange('{request.CompanyId}','{startDate}','{endDate}')"; ;

            var data = await _connection.QueryAsync<ReportAttendanceProcessedDataVM>(query);
            return data.ToList();
        }
    }
}
