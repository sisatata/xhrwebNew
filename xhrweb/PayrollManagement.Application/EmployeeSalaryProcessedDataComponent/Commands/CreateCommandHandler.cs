using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeSalaryProcessedDataComponent, EmployeeSalaryProcessedDataComponentCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedDataComponent, Guid>
        _repository;

        public async Task<EmployeeSalaryProcessedDataComponentCommandVM> Handle(Commands.V1.CreateEmployeeSalaryProcessedDataComponent request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryProcessedDataComponentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                //var entity = PayrollEntities.EmployeeSalaryProcessedDataComponent.Create(
                //         request.EmployeeSalaryProcessedDataId,
                //         request.EmployeeSalaryComponentId,
                //         request.BenefitDeductionId,
                //         request.ComponentValue,
                //         request.Type,
                //         request.CompanyId,
                //         request.IsDeleted


                //);
                //var data = await _repository.AddAsync(entity);

                //response.Status = true;
                //response.Message = "entity created successfully.";
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
