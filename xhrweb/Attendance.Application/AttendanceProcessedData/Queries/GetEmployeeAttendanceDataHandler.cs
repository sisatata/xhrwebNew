using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.AttendanceProcessedData.Queries
{
    public class GetEmployeeAttendanceDataHandler : IRequestHandler<Queries.GetEmployeeAttendanceData, List<AttendanceProcessedDataVM>>
    {
        #region ctor
        public GetEmployeeAttendanceDataHandler(DbConnection connection, ICurrentUserContext userContext)
        {
            _connection = connection;
            _userContext = userContext;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        #endregion
        #region methods
        public async Task<List<AttendanceProcessedDataVM>> Handle(Queries.GetEmployeeAttendanceData request, CancellationToken cancellationToken)
        {
            DateTime format = (DateTime)request.EndDate;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartDate;
            string startDate = format.ToString("yyyy-MM-dd");
            
            var query = "";
           
            query = $"SELECT * from attendance.GetAttendanceProcessedDataListByCompany('{request.CompanyId}','{startDate}','{endDate}')"; ;

            try
            {
                  var data = await _connection.QueryAsync<AttendanceProcessedDataVM>(query);
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
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    request.SearchText = request.SearchText.Trim();
                    var nameList = new List<string>();
                    var emailList = new List<string>();
                    var idList = new List<string>();
                    foreach (var item in data)
                    {
                        bool employeeFullNameFound = item.EmployeeName.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeFullNameFound)
                        {

                            nameList.Add(item.EmployeeName);
                        }

                        bool employeeEmailFound = item.LoginId.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeEmailFound)
                        {

                            emailList.Add(item.LoginId);
                        }
                        bool employeeIdFound = item.EmployeeCompanyId.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeIdFound)
                        {

                            idList.Add(item.EmployeeCompanyId);
                        }
                    }
                    data = data.Where(r => emailList.Contains(r.LoginId) || nameList.Contains(r.EmployeeName) || idList.Contains(r.EmployeeCompanyId));
                }

                data = data.OrderByDescending(x => x.DepartmentId).ThenByDescending(x => x.EmployeeId);
                return data.ToList();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }



            
        }
        #endregion
    }
}
