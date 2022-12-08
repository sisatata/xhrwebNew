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
    public class GetEmployeePagedListQueryHandler : IRequestHandler<Queries.GetEmployeePagedList, PagedResult<EmployeeVM>>
    {
        public GetEmployeePagedListQueryHandler(DbConnection connection,
            ICurrentUserContext userContext, IUriComposer uriComposer)
        {
            _connection = connection;
            _userContext = userContext;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        private readonly IUriComposer _uriComposer;
        public async Task<PagedResult<EmployeeVM>> Handle(Queries.GetEmployeePagedList request, CancellationToken cancellationToken)
        {
            string companyId = _userContext.CurrentUserCompanyId;
            var query = $"SELECT * from employee.GetEmployeeListByCompany('{companyId}')"; ;
            try
            {
                var data = await _connection.QueryAsync<EmployeeVM>(query);
                data = data.OrderByDescending(x => x.DepartmentId).ThenByDescending(x => x.EmployeeId);
                var result = data.AsQueryable().GetPagedList<EmployeeVM>(request.PageNumber, request.PageSize, request.GetAll);
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
    }
}






