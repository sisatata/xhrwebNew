using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeSalaryProcessedDataComponent, EmployeeSalaryProcessedDataComponentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid> _repository;

        public async Task<EmployeeSalaryProcessedDataComponentCommandVM>
            Handle(Commands.V1.UpdateEmployeeSalaryProcessedDataComponent request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryProcessedDataComponentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
    //            var entity = await _repository.GetByIdAsync(request.Id);
    //            entity.UpdateEmployeeSalaryProcessedDataComponent(

    //                     request.EmployeeSalaryProcessedDataId,
    //                     request.EmployeeSalaryComponentId,
    //                     request.BenefitDeductionId,
    //                     request.ComponentValue,
    //                     request.Type,
    //                     request.CompanyId,
    //                     request.IsDeleted

    //);

    //            await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

