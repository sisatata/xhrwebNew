using ASL.Hrms.SharedKernel.Interfaces;
using Dapper;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.Employee.Queries
{
    public class GetEmployeeFilteredListWithoutPaginationQueryHandler : IRequestHandler<Queries.GetEmployeeFilteredListWithoutPagination, List<EmployeeVM>>
    {
        public GetEmployeeFilteredListWithoutPaginationQueryHandler(DbConnection connection, ICurrentUserContext userContext, IUriComposer uriComposer)
        {
            _connection = connection;
            _userContext = userContext;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        private readonly IUriComposer _uriComposer;

        public async Task<List<EmployeeVM>> Handle(Queries.GetEmployeeFilteredListWithoutPagination request, CancellationToken cancellationToken)
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

                // var result = data.AsQueryable().GetPagedList(request.PageNumber, request.PageSize, request.GetAll);

                return data.Select(x => new EmployeeVM { Id=x.Id, FullName=x.FullName }).ToList();
            }
            catch(Exception ex)
            {
                throw;
            }

            }
            
    }
}
