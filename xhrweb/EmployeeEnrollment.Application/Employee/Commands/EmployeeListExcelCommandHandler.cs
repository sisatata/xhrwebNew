using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Utility.ExtensionMethods;
using ASL.Utility.FileManager.Interfaces;

using Dapper;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EmployeeEnrollment.Application.Employee.Commands.Commands.V1;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public class EmployeeListExcelCommandHandler : IRequestHandler<Commands.V1.EmployeeListExport, List<EmployeeExportVM>>
    {
        #region ctor
        public EmployeeListExcelCommandHandler(DbConnection connection, ICurrentUserContext userContext)
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
        public async Task<List<EmployeeExportVM>> Handle(Commands.V1.EmployeeListExport request, CancellationToken cancellationToken)
        {
            string companyId = _userContext.CurrentUserCompanyId;
            var query = $"SELECT * from employee.GetEmployeeListByCompany('{companyId}')"; ;
            try
            {
                var data = await _connection.QueryAsync<EmployeeVM>(query);

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
                        bool employeeFullNameFound = item.FullName.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeFullNameFound)
                        {

                            nameList.Add(item.FullName);
                        }

                        bool employeeEmailFound = item.LoginId.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeEmailFound)
                        {

                            emailList.Add(item.LoginId);
                        }
                        bool employeeIdFound = item.EmployeeId.ToLower().Contains(request.SearchText.ToLower());
                        if (employeeIdFound)
                        {

                            idList.Add(item.EmployeeId);
                        }
                    }
                    data = data.Where(r => emailList.Contains(r.LoginId) || nameList.Contains(r.FullName) || idList.Contains(r.EmployeeId));
                }
                data = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x=>x.FullName);

                List<EmployeeExportVM> employeesExportVM = new List<EmployeeExportVM>();
                employeesExportVM = data.Select(x => new EmployeeExportVM {EmployeeId=x.EmployeeId, FullName=x.FullName,BranchName = x.BranchName, DepartmentName = x.DepartmentName, PositionName = x.PositionName, DateOfBirth = x.DateOfBirth,  NationalityName = x.NationalityName, GenderName = x.GenderName, GradeName = x.GradeName, LoginId
                 = x.LoginId}).ToList();
                return employeesExportVM;




            }

            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }

        #endregion
    }
}
