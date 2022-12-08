using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ConfirmationRule.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateConfirmationRule, ConfirmationRuleCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<RuleEntities.ConfirmationRule, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.ConfirmationRule, Guid>
        _repository;

        public async Task<ConfirmationRuleCommandVM> Handle(Commands.V1.CreateConfirmationRule request, CancellationToken cancellationToken)
        {
            var response = new ConfirmationRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = RuleEntities.ConfirmationRule.Create(
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
