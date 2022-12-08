using Dapper;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveApplications.Queries
{
    public class GetLeaveDataToSendEmailQueryHandler : IRequestHandler<Queries.GetLeaveDataToSendEmail, List<LeaveDataForEmailVM>>
    {
        public GetLeaveDataToSendEmailQueryHandler(IReferenceRepository<Company, Guid> companyRepository, DbConnection connection)
        {
            _connection = connection;
            _companyRepository = companyRepository;
        }

        private readonly DbConnection _connection;
        private readonly IReferenceRepository<Company, Guid> _companyRepository;

        public async Task<List<LeaveDataForEmailVM>> Handle(Queries.GetLeaveDataToSendEmail request, CancellationToken cancellationToken)
        {
            try
            {
                var data = new List<LeaveDataForEmailVM>();

                var activeCompanies = await _companyRepository.GetAll().ConfigureAwait(false);
                foreach (var company in activeCompanies)
                {
                    var query = $"SELECT * from leave.GetLeaveDataListByCompanyAndDate('{company.Id}', '{DateTime.Now.ToString("yyyy-MM-dd")}')";
                    var d = await _connection.QueryAsync<LeaveDataForEmailVM>(query);
                    data.AddRange(d.ToList());
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
