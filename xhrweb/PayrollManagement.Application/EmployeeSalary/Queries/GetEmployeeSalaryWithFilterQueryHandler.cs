using Dapper;
using MediatR;
using PayrollManagement.Application.EmployeeSalary.Queries.Models;
using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeSalary.Queries
{
    public class GetEmployeeSalaryWithFilterQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryWithFilter, List<EmployeeSalaryVM>>
    {
        #region ctor
        public GetEmployeeSalaryWithFilterQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<List<EmployeeSalaryVM>> Handle(Queries.GetEmployeeSalaryWithFilter request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetCurrentSalary ('{request.CompanyId}')";
            try
            {
                var data = await _connection.QueryAsync<EmployeeSalaryVM>(query).ConfigureAwait(false);
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
                        bool employeeFullNameFound = item.FullName.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeFullNameFound)
                        {

                            nameList.Add(item.FullName);
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
                    data = data.Where(r => emailList.Contains(r.LoginId) || nameList.Contains(r.FullName) || idList.Contains(r.CompanyEmployeeId));
                }
                foreach (var item in data.ToList())
                {
                    var q = $"SELECT * from payroll.GetEmployeeSalaryComponentListByParent ('{item.Id}')";
                    var d = await _connection.QueryAsync<EmployeeSalaryComponentVM>(q);
                    item.EmployeeSalaryComponentList = d.ToList();
                }
                data = data.OrderByDescending(x => x.DepartmentId).ThenByDescending(x => x.EmployeeId);
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
