using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeManager.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeManager, EmployeeManagerCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeManager, Guid>
        repository, ICurrentUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeManager, Guid>
        _repository;
        private readonly ICurrentUserContext _userContext;

        public async Task<EmployeeManagerCommandVM> Handle(Commands.V1.CreateEmployeeManager request, CancellationToken cancellationToken)
        {
            var response = new EmployeeManagerCommandVM
            {
                Status = false,
                Message = "error"
            };


            try
            {
                var employeeManagerFilter = new EmployeeManagerTypeFilterSpecification(request.EmployeeId, new Guid(_userContext.CurrentUserCompanyId), request.ManagerId.Value, request.ManagerTypeId.Value);
                var oList = await _repository.ListAsync(employeeManagerFilter).ConfigureAwait(false);
                if (oList.Any())
                {
                    response.Message = "Duplicate records cannot be inserted";
                    return response;
                }
                var entity = EmployeeEntities.EmployeeManager.Create(
                     request.EmployeeId,
                     request.ManagerId,
                     request.IsPrimaryManager,
                     request.AssignedBy,
                     request.AssignDate,
                     //request.UnAssignedBy,
                     //request.UnAssignDate,
                     //request.IsDeleted,
                     new Guid(_userContext.CurrentUserCompanyId),
                     request.ManagerTypeId
            );
                var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
