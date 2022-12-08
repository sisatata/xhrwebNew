using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRuleDetail.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateSalaryRuleDetail, SalaryRuleDetailCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid> _repository;

        public async Task<SalaryRuleDetailCommandVM>
            Handle(Commands.V1.UpdateSalaryRuleDetail request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateSalaryRuleDetail(
                         request.SalaryRuleId,
                         request.SalaryHead,
                         request.Value,
                         request.ValueType,
                         request.PercentDependOn

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

