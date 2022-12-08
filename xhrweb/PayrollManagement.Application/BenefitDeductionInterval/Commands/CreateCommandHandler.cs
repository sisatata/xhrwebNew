using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitDeductionInterval.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBenefitDeductionInterval, BenefitDeductionIntervalCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionInterval, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionInterval, Guid>
        _repository;

        public async Task<BenefitDeductionIntervalCommandVM> Handle(Commands.V1.CreateBenefitDeductionInterval request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionIntervalCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.BenefitDeductionInterval.Create(
                         request.IntervalId,
                         request.IntervalName


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
