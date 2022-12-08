using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRuleDetail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateSalaryRuleDetail, SalaryRuleDetailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid>
        _repository;

        public async Task<SalaryRuleDetailCommandVM> Handle(Commands.V1.CreateSalaryRuleDetail request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = RuleEntities.SalaryRuleDetail.Create(
                         request.SalaryRuleId,
                         request.SalaryHead,
                         request.Value,
                         request.ValueType,
                         request.PercentDependOn
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
