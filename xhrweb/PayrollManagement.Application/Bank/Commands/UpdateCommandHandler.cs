using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.Bank.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBank, BankCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.Bank, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.Bank, Guid> _repository;

        public async Task<BankCommandVM>
            Handle(Commands.V1.UpdateBank request, CancellationToken cancellationToken)
        {
            var response = new BankCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateBank(

                         request.BankName,
                         request.BankNameLC,
                         request.SortOrder,
                         request.CompanyId,
                         request.IsDeleted,
                         request.PaymentOptionId

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

