using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeePaidIncomeTax, EmployeePaidIncomeTaxCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.EmployeePaidIncomeTax, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeePaidIncomeTax, Guid> _repository;

        public async Task<EmployeePaidIncomeTaxCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeePaidIncomeTax request, CancellationToken cancellationToken)
        {
            var response = new EmployeePaidIncomeTaxCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                //entity.MarkAsDelete();

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
