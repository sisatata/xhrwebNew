using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Interfaces;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteBenefitBillClaim, BenefitBillClaimCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> repository,
            IActivityLogService activityLogService)
        {
            _repository = repository;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> _repository;
        private readonly IActivityLogService _activityLogService;

        public async Task<BenefitBillClaimCommandVM>
            Handle(Commands.V1.MarkAsDeleteBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var response = new BenefitBillClaimCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteBenefitBillClaim();

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
