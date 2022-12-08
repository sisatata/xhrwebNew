using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionCode.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBenefitDeductionCode, BenefitDeductionCodeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionCode, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionCode, Guid> _repository;

        public async Task<BenefitDeductionCodeCommandVM>
            Handle(Commands.V1.UpdateBenefitDeductionCode request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionCodeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBenefitDeductionCode(
                         request.CompanyId,
                         request.BenifitDeductionCode,
                         request.BenifitDeductionCodeName

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

