using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeePFTransaction.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeePFTransaction, EmployeePFTransactionCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeePFTransaction, Guid> _repository;

        public async Task<EmployeePFTransactionCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeePFTransaction request, CancellationToken cancellationToken)
        {
            var response = new EmployeePFTransactionCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteEmployeePFTransaction();

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
