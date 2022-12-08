using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRule.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteSalaryRule, SalaryRuleCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<RuleEntities.SalaryRule, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRule, Guid> _repository;

        public async Task<SalaryRuleCommandVM>
            Handle(Commands.V1.MarkAsDeleteSalaryRule request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteSalaryRule();

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
