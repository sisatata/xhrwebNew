using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeBankAccount.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeBankAccount, EmployeeBankAccountCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid> _repository;

        public async Task<EmployeeBankAccountCommandVM>
            Handle(Commands.V1.UpdateEmployeeBankAccount request, CancellationToken cancellationToken)
        {
            var response = new EmployeeBankAccountCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateEmployeeBankAccount(

                         request.BankId,
                         request.BankBranchId,
                         request.AccountNo,
                         request.AccountTitle,
                         request.IsPrimary,
                         request.CompanyId,
                         request.PaymentOptionId,
                         request.StartDate,
                         request.EndDate,
                         request.Remarks,
                         request.EmployeeId

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

