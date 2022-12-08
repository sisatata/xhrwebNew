using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ConfirmationRule.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteConfirmationRule, ConfirmationRuleCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<RuleEntities.ConfirmationRule, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.ConfirmationRule, Guid> _repository;

        public async Task<ConfirmationRuleCommandVM>
            Handle(Commands.V1.MarkAsDeleteConfirmationRule request, CancellationToken cancellationToken)
        {
            var response = new ConfirmationRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteConfirmationRule();

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
