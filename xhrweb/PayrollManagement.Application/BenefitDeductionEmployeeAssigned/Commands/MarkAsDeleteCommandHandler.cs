using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteBenefitDeductionEmployeeAssigned, BenefitDeductionEmployeeAssignedCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionEmployeeAssigned, Guid> _repository;

        public async Task<BenefitDeductionEmployeeAssignedCommandVM>
            Handle(Commands.V1.MarkAsDeleteBenefitDeductionEmployeeAssigned request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionEmployeeAssignedCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteBenefitDeductionEmployeeAssigned();

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity deleted successfully.";
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
