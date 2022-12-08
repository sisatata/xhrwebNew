using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeSalaryComponent, EmployeeSalaryComponentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryComponent, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryComponent, Guid> _repository;

        public async Task<EmployeeSalaryComponentCommandVM>
            Handle(Commands.V1.UpdateEmployeeSalaryComponent request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryComponentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateEmployeeSalaryComponent(
                         request.EmployeeSalaryId,
                         request.SalaryStructureComponentId,
                         request.ComponentAmount,
                         request.CompanyId,
                         request.IsDeleted

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

