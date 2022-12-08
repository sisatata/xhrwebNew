using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
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
    public class GetAttendanceMissPunchByCompanyDataQueryHandler : IRequestHandler<Queries.AttendanceMissPunchEmailSendToAdmin, List<MissPunchAttendanceDataVM>>
    {
        public GetAttendanceMissPunchByCompanyDataQueryHandler(IReferenceRepository<Company, Guid> companyRepository, DbConnection connection)
        {
            _connection = connection;
            _companyRepository = companyRepository;
        }
        private readonly DbConnection _connection;
        private readonly IReferenceRepository<Company, Guid> _companyRepository;

        public async Task<List<MissPunchAttendanceDataVM>> Handle(Queries.AttendanceMissPunchEmailSendToAdmin request, CancellationToken cancellationToken)
        {
            var data = new List<MissPunchAttendanceDataVM>();

            var activeCompanies = await _companyRepository.GetAll().ConfigureAwait(false);
            foreach (var company in activeCompanies)
            {
                var query = $"SELECT * from attendance.GetAttendanceMissPunchDataListByCompanyAndDate('{company.Id}','{DateTime.Now.ToString("yyyy-MM-dd")}')"; ;

                var d = await _connection.QueryAsync<MissPunchAttendanceDataVM>(query);
                data.AddRange(d.ToList());
            }

            return data;
        }
    }
}
