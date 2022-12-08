using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.PaymentOption.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdatePaymentOption, PaymentOptionCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.PaymentOption, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.PaymentOption, Guid> _repository;

        public async Task<PaymentOptionCommandVM>
            Handle(Commands.V1.UpdatePaymentOption request, CancellationToken cancellationToken)
        {
            var response = new PaymentOptionCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdatePaymentOption(

                         request.PaymentOptionId,
                         request.OptionName,
                         request.OptionCode,
                         request.SortOrder,
                         request.IsDeleted

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

