using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxInvestment.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateIncomeTaxInvestment, IncomeTaxInvestmentCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.IncomeTaxInvestment, Guid>
        _repository;

        public async Task<IncomeTaxInvestmentCommandVM> Handle(Commands.V1.CreateIncomeTaxInvestment request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxInvestmentCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.IncomeTaxInvestment.Create(
                         request.InvestmentPercentage,
                         request.WaiverPercentage,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks,
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
