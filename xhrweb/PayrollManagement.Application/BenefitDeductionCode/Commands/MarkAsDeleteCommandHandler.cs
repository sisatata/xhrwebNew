using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayroEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionCode.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteBenefitDeductionCode, BenefitDeductionCodeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayroEntities.BenefitDeductionCode, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayroEntities.BenefitDeductionCode, Guid> _repository;

        public async Task<BenefitDeductionCodeCommandVM>
            Handle(Commands.V1.MarkAsDeleteBenefitDeductionCode request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionCodeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteBenefitDeductionCode();

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
