using EmployeeEnrollment.Application.Employee.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Models;

namespace EmployeeEnrollment.Application.Employee.Queries
{
    public class GetEmployeeFilteredListQueryHandler : IRequestHandler<Queries.GetEmployeeFilteredList, PagedResult<EmployeeVM>>
    {
        #region ctor
        public GetEmployeeFilteredListQueryHandler(DbConnection connection,
            ICurrentUserContext userContext, IUriComposer uriComposer)
        {
            _connection = connection;
            _userContext = userContext;
            _uriComposer = uriComposer;
        }
        #endregion

        #region properties
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        private readonly IUriComposer _uriComposer;
        #endregion

        #region methods

        public async Task<PagedResult<EmployeeVM>> Handle(Queries.GetEmployeeFilteredList request, CancellationToken cancellationToken)
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
                        bool employeeFullNameFound  = item.FullName.ToLower().Contains(request.SearchText.ToLower());
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
                data = data.OrderBy(x => x.BranchName).ThenBy(x=>x.DepartmentName).ThenBy(x=>x.DesignationOrder).ThenBy(x=>x.FullName);
               var result = data.AsQueryable().GetPagedList(request.PageNumber, request.PageSize, request.GetAll);
                foreach (var item in result.data)
                {
                    // Combining real path
                    if (item != null && !string.IsNullOrWhiteSpace(item.EmployeeImageUri))
                    {
                        item.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(item.EmployeeImageUri);
                    }

                }
                return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        #endregion
    }
}






