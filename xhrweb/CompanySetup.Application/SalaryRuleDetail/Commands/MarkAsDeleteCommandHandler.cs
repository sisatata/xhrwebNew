using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using RuleEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.SalaryRuleDetail.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteSalaryRuleDetail, SalaryRuleDetailCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<RuleEntities.SalaryRuleDetail, Guid> _repository;

        public async Task<SalaryRuleDetailCommandVM>
            Handle(Commands.V1.MarkAsDeleteSalaryRuleDetail request, CancellationToken cancellationToken)
        {
            var response = new SalaryRuleDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteSalaryRuleDetail();

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
