using CompanySetup.Application.Designation.Queries.Models;
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

namespace CompanySetup.Application.Designation.Queries
{
    public class GetDesignationListByEntitiesQueryHandler : IRequestHandler<Queries.GetDesignationListByEntities, List<DesignationVM>>
    {
        public GetDesignationListByEntitiesQueryHandler(DbConnection connection,
            IAsyncRepository<Core.Entities.Designation, Guid> repository,
             IAsyncRepository<Core.Entities.Department, Guid> repositoryDept)
        {
            _connection = connection;
            _repository = repository;
            _repositoryDept = repositoryDept;
        }

        private readonly DbConnection _connection;
        private readonly IAsyncRepository<Core.Entities.Designation, Guid> _repository;
        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repositoryDept;

        public async Task<List<DesignationVM>> Handle(Queries.GetDesignationListByEntities request, CancellationToken cancellationToken)
        {
            var queryDept = new DepartmentFilterSpecification(request.CompanyId);
            var departments = await _repositoryDept.ListAsync(queryDept).ConfigureAwait(false);

            var query = new DesignationFilterSpecification();
            var designations = await _repository.ListAsync(query).ConfigureAwait(false);

            if (request.EntityIds == null || request.EntityIds.Count < 1)
            {
                return (from d in designations
                        join dept in departments on d.LinkedEntityId equals dept.Id
                        select new DesignationVM
                        {
                            Id = d.Id,
                            CompanyId = dept.CompanyId,
                            BranchId = dept.BranchId,
                            DepartmentId = dept.Id,
                            DepartmentName = dept.DepartmentName,
                            DesignationName = d.DesignationName,
                            ShortName = d.ShortName,
                            SortOrder = d.SortOrder
                        }).ToList();
            }
            var filterdDepts = (from d in designations
                                join dept in departments on d.LinkedEntityId equals dept.Id
                                join dp in request.EntityIds on d.LinkedEntityId equals dp
                                select new DesignationVM
                                {
                                    Id = d.Id,
                                    CompanyId = dept.CompanyId,
                                    BranchId = dept.BranchId,
                                    DepartmentId = dept.Id,
                                    DepartmentName = dept.DepartmentName,
                                    DesignationName = d.DesignationName,
                                    ShortName = d.ShortName,
                                    SortOrder = d.SortOrder
                                }).ToList();
            return filterdDepts;
        }
    }
}
