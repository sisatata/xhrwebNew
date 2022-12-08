using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.IncomeTaxPayerType.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteIncomeTaxPayerType, IncomeTaxPayerTypeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.IncomeTaxPayerType, Guid> _repository;

        public async Task<IncomeTaxPayerTypeCommandVM>
            Handle(Commands.V1.MarkAsDeleteIncomeTaxPayerType request, CancellationToken cancellationToken)
        {
            var response = new IncomeTaxPayerTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteIncomeTaxPayerType();

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
