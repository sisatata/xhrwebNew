using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BonusEntities = PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using PayrollManagement.Core.Specifications;

namespace PayrollManagement.Application.BonusConfiguration.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBonusConfiguration, BonusConfigurationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<BonusEntities.BonusConfiguration, Guid>repository, IServiceBus serviceBus)
        {
            _repository = repository;
            _serviceBus = serviceBus;
        }

        private readonly IAsyncRepository<BonusEntities.BonusConfiguration, Guid> _repository;
        private readonly IServiceBus _serviceBus;

        public async Task<BonusConfigurationCommandVM> Handle(Commands.V1.CreateBonusConfiguration request, CancellationToken cancellationToken)
        {
            var response = new BonusConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var bonusConfigurationDataFilter = new BonusConfigurationByCompanyFilterSpecification(request.CompanyId.Value);
                var bonusConfigurationData = await _repository.ListAsync(bonusConfigurationDataFilter).ConfigureAwait(false);

                var bonusConfigurationAggregate = new BonusConfigurationAggregate(bonusConfigurationData);
                bonusConfigurationAggregate.NewBonusConfiguration(request.CompanyId,
                         request.ReligionId,
                         request.RangeFromInMonth,
                         request.RangeToInMonth,
                         request.BasicOrGrossId,
                         request.PercentOfBasicOrGross,
                         request.FixedAmount,
                         request.IsPaidFull,
                         request.PartialOnId,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks);

                var entity = await _repository.AddAsync(bonusConfigurationAggregate.BonusConfiguration).ConfigureAwait(false);

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
