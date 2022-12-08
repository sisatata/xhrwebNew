using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BonusEntities = PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Specifications;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;

namespace PayrollManagement.Application.BonusConfiguration.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBonusConfiguration, BonusConfigurationCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<BonusEntities.BonusConfiguration, Guid> repository, IServiceBus serviceBus)
        {
            _repository = repository;
            _serviceBus = serviceBus;
        }

        private readonly IAsyncRepository<BonusEntities.BonusConfiguration, Guid> _repository;
        private readonly IServiceBus _serviceBus;

        public async Task<BonusConfigurationCommandVM>
            Handle(Commands.V1.UpdateBonusConfiguration request, CancellationToken cancellationToken)
        {
            var response = new BonusConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entityToUpdate = await _repository.GetByIdAsync(request.Id.Value);

                var bonusConfigurationDataFilter = new BonusConfigurationByCompanyFilterSpecification(request.CompanyId.Value);
                var bonusConfigurationData = await _repository.ListAsync(bonusConfigurationDataFilter).ConfigureAwait(false);

                var bonusConfigurationAggregate = new BonusConfigurationAggregate(bonusConfigurationData);

                bonusConfigurationAggregate.UpdateBonusConfiguration(entityToUpdate, request.CompanyId,
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

                await _repository.UpdateAsync(bonusConfigurationAggregate.BonusConfiguration).ConfigureAwait(false);

                response.Status = true;
                response.Message = "entity updated successfully.";
                response.Id = entityToUpdate.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

