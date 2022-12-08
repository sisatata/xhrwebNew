using Dapper;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
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
  public  class GetLeaveApplicationByCompanyQueryHandler : IRequestHandler<Queries.GetLeaveApplicationByCompany,List<LeaveApplicationVM>>
    {
        public GetLeaveApplicationByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<LeaveApplicationVM>> Handle(Queries.GetLeaveApplicationByCompany request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.StartDate.HasValue)
                {
                    request.StartDate = DateTime.Now.Date.AddDays(-30);
                }

                if (!request.EndDate.HasValue)
                {
                    request.EndDate = DateTime.Now;
                }
                DateTime format = (DateTime)request.EndDate;
                string endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                string startDate = format.ToString("yyyy-MM-dd");
                var query = $"SELECT * from leave.GetLeaveApplicationByCompany('{request.CompanyId}','{startDate}', '{endDate}')";
                var data = await _connection.QueryAsync<LeaveApplicationVM>(query);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
