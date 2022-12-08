using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBenefitDeductionEmployeeAssigned, BenefitDeductionEmployeeAssignedCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid> _repository;

        public async Task<BenefitDeductionEmployeeAssignedCommandVM>
            Handle(Commands.V1.UpdateBenefitDeductionEmployeeAssigned request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionEmployeeAssignedCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBenefitDeductionEmployeeAssigned(


                         request.BenefitDeductionId,
                         request.EmployeeId,
                         request.Remarks,
                         request.IsDeleted,
                         request.StartDate,
                         request.EndDate,
                         request.Amount

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

