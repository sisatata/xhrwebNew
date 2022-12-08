using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeSalaryProcessedData, EmployeeSalaryProcessedDataCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.EmployeeSalaryProcessedData, Guid> _repository;

        public async Task<EmployeeSalaryProcessedDataCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeeSalaryProcessedData request, CancellationToken cancellationToken)
        {
            var response = new EmployeeSalaryProcessedDataCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteEmployeeSalaryProcessedData();

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity deleted successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
