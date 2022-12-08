using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.PaymentOption.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreatePaymentOption, PaymentOptionCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.PaymentOption, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.PaymentOption, Guid>
        _repository;

        public async Task<PaymentOptionCommandVM> Handle(Commands.V1.CreatePaymentOption request, CancellationToken cancellationToken)
        {
            var response = new PaymentOptionCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.PaymentOption.Create(
                         request.PaymentOptionId,
                         request.OptionName,
                         request.OptionCode,
                         request.SortOrder,
                         request.IsDeleted


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
