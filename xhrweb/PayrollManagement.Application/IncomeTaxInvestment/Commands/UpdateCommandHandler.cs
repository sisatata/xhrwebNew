using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrolleEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxInvestment.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateIncomeTaxInvestment, IncomeTaxInvestmentCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrolleEntities.IncomeTaxInvestment, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrolleEntities.IncomeTaxInvestment, Guid> _repository;

        public async Task<IncomeTaxInvestmentCommandVM>
            Handle(Commands.V1.UpdateIncomeTaxInvestment request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxInvestmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateIncomeTaxInvestment(

                         request.InvestmentPercentage,
                         request.WaiverPercentage,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks,
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

