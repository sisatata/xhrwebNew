using Dapper;
using LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary
{
    public class GetLeaveBalanceSummaryQueryHandler : IRequestHandler<LeaveBalanceSummary.GetLeaveBalanceSummary, List<LeaveBalanceSummaryVM>>
    {

        public GetLeaveBalanceSummaryQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<List<LeaveBalanceSummaryVM>> Handle(LeaveBalanceSummary.GetLeaveBalanceSummary request, CancellationToken cancellationToken)
        {
           
                var query = $"SELECT * from leave.GetLeaveBalanceSummary('{request.CompanyId }','{request.LeaveCalendar}')";            

            try
            {
                var data = await _connection.QueryAsync<LeaveBalanceSummaryVM>(query).ConfigureAwait(false);
                if (request.BranchIds != null && request.BranchIds.Count > 0)
                {
                    data = data.Where(r => request.BranchIds.Contains(r.BranchId.Value));
                }

                if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
                {
                    data = data.Where(r => request.DepartmentIds.Contains(r.DepartmentId.Value));
                }

                if (request.DesignationIds != null && request.DesignationIds.Count > 0)
                {
                    data = data.Where(r => request.DesignationIds.Contains(r.PositionId.Value));
                }
                if (request.EmployeeId != null && request.EmployeeId != Guid.Empty)
                {
                    data = data.Where(r => r.EmployeeId == request.EmployeeId);
                }
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    request.SearchText = request.SearchText.Trim();
                    var nameList = new List<string>();
                    var emailList = new List<string>();
                    var idList = new List<string>();
                    foreach (var item in data)
                    {
                        bool employeeFullNameFound = item.EmployeeName.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeFullNameFound)
                        {

                            nameList.Add(item.EmployeeName);
                        }

                        bool employeeEmailFound = item.LoginId.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeEmailFound)
                        {

                            emailList.Add(item.LoginId);
                        }
                        bool employeeIdFound = item.CompanyEmployeeId.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeIdFound)
                        {

                            idList.Add(item.CompanyEmployeeId);
                        }
                    }
                    data = data.Where(r => emailList.Contains(r.LoginId) || nameList.Contains(r.EmployeeName) || idList.Contains(r.CompanyEmployeeId));
                }

                data = data.OrderByDescending(x => x.EmployeeId).ThenByDescending(x => x.LeaveTypeName);
                return data.ToList();
            }

            catch (System.Exception ex)
            {

                throw new ArgumentException(ex.ToString());
            }

        }
    }
}
