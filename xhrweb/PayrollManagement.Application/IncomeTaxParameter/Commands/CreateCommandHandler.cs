using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxParameter.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateIncomeTaxParameter, IncomeTaxParameterCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.IncomeTaxParameter, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.IncomeTaxParameter, Guid>
        _repository;

        public async Task<IncomeTaxParameterCommandVM> Handle(Commands.V1.CreateIncomeTaxParameter request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxParameterCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.IncomeTaxParameter.Create(
                         request.ParameterName,
                         request.LimitAmount,
                         request.LimitPercentageOfBasic,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks,
                         request.IsActive,
                         request.IsDeleted,
                         request.CompanyId,
                         request.PayerTypeCode


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
