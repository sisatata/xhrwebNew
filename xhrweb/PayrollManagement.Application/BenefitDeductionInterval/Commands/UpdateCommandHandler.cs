using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionInterval.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBenefitDeductionInterval, BenefitDeductionIntervalCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionInterval, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionInterval, Guid> _repository;

        public async Task<BenefitDeductionIntervalCommandVM>
            Handle(Commands.V1.UpdateBenefitDeductionInterval request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionIntervalCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBenefitDeductionInterval(


                         request.IntervalId,
                         request.IntervalName

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

