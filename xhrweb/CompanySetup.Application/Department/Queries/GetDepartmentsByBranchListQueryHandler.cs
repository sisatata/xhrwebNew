using CompanySetup.Application.Department.Queries.Models;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Queries
{
    public class GetDepartmentsByBranchListQueryHandler : IRequestHandler<Queries.GetDepartmentsByBranchList, List<DepartmentVM>>
    {
        public GetDepartmentsByBranchListQueryHandler(DbConnection connection, IAsyncRepository<Core.Entities.Department, Guid> repository)
        {
            _connection = connection;
            _repository = repository;
        }

        private readonly DbConnection _connection;
        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repository;

        public async Task<List<DepartmentVM>> Handle(Queries.GetDepartmentsByBranchList request, CancellationToken cancellationToken)
        {
            try
            {
                var query = new DepartmentFilterSpecification(request.CompanyId);
                var departments = await _repository.ListAsync(query).ConfigureAwait(false);
                if (request.BranchIds == null || request.BranchIds.Count < 1)
                {
                    return (from d in departments
                            select new DepartmentVM
                            {
                                Id = d.Id,
                                CompanyId = d.CompanyId,
                                BranchId = d.BranchId,
                                DepartmentName = d.DepartmentName,
                                DepartmentLocalizedName = d.DepartmentLocalizedName,
                                ShortName = d.ShortName,
                                SortOrder = d.SortOrder
                            }).ToList();
                }
                var filterdDepts = (from d in departments
                                    join dp in request.BranchIds on d.BranchId equals dp
                                    select new DepartmentVM
                                    {
                                        Id = d.Id,
                                        CompanyId = d.CompanyId,
                                        BranchId = d.BranchId,
                                        DepartmentName = d.DepartmentName,
                                        DepartmentLocalizedName = d.DepartmentLocalizedName,
                                        ShortName = d.ShortName,
                                        SortOrder = d.SortOrder
                                    }).ToList();
                return filterdDepts;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
