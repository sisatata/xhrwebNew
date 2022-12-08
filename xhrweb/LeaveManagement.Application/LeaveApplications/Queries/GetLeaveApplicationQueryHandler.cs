
using Dapper;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveApplications.Queries
{
    public class GetLeaveApplicationQueryHandler : IRequestHandler<Queries.GetLeaveApplication, List<LeaveApplicationVM>>
    {
        #region ctor
        public GetLeaveApplicationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<List<LeaveApplicationVM>> Handle(Queries.GetLeaveApplication request, CancellationToken cancellationToken)
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
            string endDate = format.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            format = (DateTime)request.StartDate;
            string startDate = format.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            var query = $"SELECT * from leave.GetLeaveApplication('{request.CompanyId}','{startDate}', '{endDate}')";
            try
            {
                var data = await _connection.QueryAsync<LeaveApplicationVM>(query).ConfigureAwait(false);
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
                if (request.LeaveTypeName!=null && request.LeaveTypeName.Count > 0)
                {
                    data = data.Where(r => request.LeaveTypeName.Contains(r.LeaveTypeName));
                }
                if (request.ApprovalStatusText!=null && request.ApprovalStatusText.Count > 0)
                {
                    data = data.Where(r => request.ApprovalStatusText.Contains(r.ApprovalStatus));
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

                data = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.StartDate);
                return data.ToList();
            }

            catch (System.Exception ex)
            {

                throw new ArgumentException(ex.ToString());
            }

        }
        #endregion
    }
}
