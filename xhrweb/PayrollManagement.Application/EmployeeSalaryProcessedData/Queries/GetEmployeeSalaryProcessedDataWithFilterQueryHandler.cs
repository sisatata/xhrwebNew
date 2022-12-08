using Dapper;
using MediatR;
using PayrollManagement.Application.EmployeeSalaryProcessedData.Queries.Models;
using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Queries
{
    public class GetEmployeeSalaryProcessedDataWithFilterQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryProcessedDataWithFilter, List<EmployeeSalaryProcessedDataVM>>
    {
        #region ctor
        public GetEmployeeSalaryProcessedDataWithFilterQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<List<EmployeeSalaryProcessedDataVM>> Handle(Queries.GetEmployeeSalaryProcessedDataWithFilter request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetEmployeeSalaryProcessedData ('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}')";
            try
            {
                var data = await _connection.QueryAsync<EmployeeSalaryProcessedDataVM>(query).ConfigureAwait(false);
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
                    var q = $"SELECT * from payroll.GetEmployeeSalaryProcessedDataComponentByParent ('{item.Id}')";
                    var d = await _connection.QueryAsync<EmployeeSalaryProcessedDataComponentVM>(q);
                    item.EmployeeSalaryProcessedDataComponentList = d.ToList();
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
