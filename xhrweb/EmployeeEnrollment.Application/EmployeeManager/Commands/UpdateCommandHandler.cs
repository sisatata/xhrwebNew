using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeManager.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeManager, EmployeeManagerCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeManager, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeManager, Guid> _repository;

        public async Task<EmployeeManagerCommandVM>
            Handle(Commands.V1.UpdateEmployeeManager request, CancellationToken cancellationToken)
        {
            var response = new EmployeeManagerCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var employeeManagerFilter = new EmployeeManagerTypeFilterSpecification(request.EmployeeId, request.CompanyId.Value, request.ManagerId.Value, request.ManagerTypeId.Value);
                var oList = await _repository.ListAsync(employeeManagerFilter).ConfigureAwait(false);
                if (oList.Any() && oList.FirstOrDefault(r => r.Id != request.Id) != null)
                {
                    response.Message = "Record duplicated with other.";
                    return response;
                }

                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeManager(
                         request.EmployeeId,
                         request.ManagerId,
                         request.IsPrimaryManager,
                         request.AssignedBy,
                         request.AssignDate,
                         //request.UnAssignedBy,
                         //request.UnAssignDate,
                         //request.IsDeleted,
                         request.CompanyId.Value,
                         request.ManagerTypeId

    );

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
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

