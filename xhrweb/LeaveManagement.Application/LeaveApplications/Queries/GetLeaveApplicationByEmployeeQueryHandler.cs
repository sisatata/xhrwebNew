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
    public class GetLeaveApplicationByEmployeeQueryHandler : IRequestHandler<Queries.GetLeaveApplicationByEmployee, LeaveApplicationSummaryVM>
    {
        public GetLeaveApplicationByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<LeaveApplicationSummaryVM> Handle(Queries.GetLeaveApplicationByEmployee request, CancellationToken cancellationToken)
        {
            LeaveApplicationSummaryVM oModel = new LeaveApplicationSummaryVM();
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
                var queryLeaveData = $"SELECT * from leave.GetEmployeeLeaveSummaryData('{request.EmployeeId}')"; ;

                var dataLeave = await _connection.QueryAsync<EmployeeLeaveVM>(queryLeaveData);
                oModel.EmployeeLeaves = dataLeave.ToList();
                oModel.EmployeeId = request.EmployeeId;

                DateTime format = (DateTime)request.EndDate;
                string endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                string startDate = format.ToString("yyyy-MM-dd");

                var query = $"SELECT * from leave.GetLeaveApplicationByEmployee('{request.EmployeeId}','{startDate}', '{endDate}')";
                var data = await _connection.QueryAsync<LeaveApplicationVM>(query);
                oModel.EmployeeLeaveApplictions = data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oModel;
        }
    }
}
