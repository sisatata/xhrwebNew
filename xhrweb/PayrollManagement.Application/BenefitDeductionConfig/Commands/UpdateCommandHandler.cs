using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;
using PayrollManagement.Core.ApplicationEvents;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PayrollManagement.Application.BenefitDeductionConfig.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBenefitDeductionConfig, BenefitDeductionConfigCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BenefitDeductionConfig, Guid> repository,
             IServiceBus serviceBus,
            IConfiguration configuration,
            ICurrentUserContext userContext)
        {
            _repository = repository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _userContext = userContext;
        }

        private readonly IAsyncRepository<PayrollEntities.BenefitDeductionConfig, Guid> _repository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserContext _userContext;

        public async Task<BenefitDeductionConfigCommandVM>
            Handle(Commands.V1.UpdateBenefitDeductionConfig request, CancellationToken cancellationToken)
        {
            var response = new BenefitDeductionConfigCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateBenefitDeductionConfig(


                         request.BenefitDeductionCode,
                         request.Name,
                         request.Description,
                         request.Type,
                         request.BasicOrGross,
                         request.CalculationType,
                         request.PercentOfBasicOrGross,
                         request.FixedAmount,
                         request.IntervalId,
                         request.CompanyId,
                         request.IsCalculateSalary,
                         request.IsActive,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted

    );

                await _repository.UpdateAsync(entity);

                var @event = new PayrollActivityLogEvent
                {
                    SystemKeyword = "Payroll",
                    Comment = $"Benefit deduction updated {request.BenefitDeductionCode} - {request.Name} for {request.StartDate} to {request.EndDate}",
                    //Entity = employeeBonusProcessAggregate,
                    CommandPublished = DateTime.Now,
                    LoginUserId = _userContext.CurrentUserId,
                    CurrentUserCompanyId = _userContext.CurrentUserCompanyId
                };
                await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);


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

