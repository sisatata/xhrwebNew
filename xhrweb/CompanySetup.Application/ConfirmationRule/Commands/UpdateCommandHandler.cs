using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ConfirmationRule.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateConfirmationRule, ConfirmationRuleCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<RuleEntities.ConfirmationRule, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.ConfirmationRule, Guid> _repository;

        public async Task<ConfirmationRuleCommandVM>
            Handle(Commands.V1.UpdateConfirmationRule request, CancellationToken cancellationToken)
        {
            var response = new ConfirmationRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateConfirmationRule(
                         request.RuleName,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
                         request.ConfirmationType,
                         request.ConfirmationMonths,
                         request.SortOrder,
                         request.IsActive,
                         request.IsDeleted,
                         request.CompanyId

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

