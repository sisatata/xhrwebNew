using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BonusEntities = PayrollManagement.Core.Entities.BonusConfigurationAggregate;

namespace PayrollManagement.Application.BonusConfiguration.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteBonusConfiguration, BonusConfigurationCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<BonusEntities.BonusConfiguration, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<BonusEntities.BonusConfiguration, Guid> _repository;

        public async Task<BonusConfigurationCommandVM>
            Handle(Commands.V1.MarkAsDeleteBonusConfiguration request, CancellationToken cancellationToken)
        {
            var response = new BonusConfigurationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteBonusConfiguration();

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
