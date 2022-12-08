using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeBankAccount.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeBankAccount, EmployeeBankAccountCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid> _repository;

        public async Task<EmployeeBankAccountCommandVM> Handle(Commands.V1.CreateEmployeeBankAccount request, CancellationToken cancellationToken)
        {
            var response = new EmployeeBankAccountCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = PayrollEntities.EmployeeBankAccount.Create(
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
