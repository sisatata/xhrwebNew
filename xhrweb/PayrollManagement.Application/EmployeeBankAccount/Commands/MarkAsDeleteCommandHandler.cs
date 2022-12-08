using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeBankAccount.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeBankAccount, EmployeeBankAccountCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeBankAccount, Guid> _repository;

        public async Task<EmployeeBankAccountCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeeBankAccount request, CancellationToken cancellationToken)
        {
            var response = new EmployeeBankAccountCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteEmployeeBankAccount();

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
