using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRule.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateSalaryRule, SalaryRuleCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<RuleEntities.SalaryRule, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRule, Guid>
        _repository;

        public async Task<SalaryRuleCommandVM> Handle(Commands.V1.CreateSalaryRule request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = RuleEntities.SalaryRule.Create(

                         request.RuleName,
                         request.Description,
                         request.StartDate,
                         request.EndDate,
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
