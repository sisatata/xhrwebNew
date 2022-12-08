using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxInvestment.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteIncomeTaxInvestment, IncomeTaxInvestmentCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid> _repository;

        public async Task<IncomeTaxInvestmentCommandVM>
            Handle(Commands.V1.MarkAsDeleteIncomeTaxInvestment request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxInvestmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteIncomeTaxInvestment();

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
