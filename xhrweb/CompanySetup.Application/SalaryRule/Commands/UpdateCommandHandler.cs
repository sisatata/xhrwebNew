using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRule.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateSalaryRule, SalaryRuleCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<RuleEntities.SalaryRule, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRule, Guid> _repository;

        public async Task<SalaryRuleCommandVM>
            Handle(Commands.V1.UpdateSalaryRule request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateSalaryRule(

                         request.RuleName,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
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

