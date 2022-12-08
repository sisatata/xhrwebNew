using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeStatusHistory, EmployeeStatusHistoryCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeStatusHistory, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeStatusHistory, Guid> _repository;

        public async Task<EmployeeStatusHistoryCommandVM>
            Handle(Commands.V1.MarkAsDeleteEmployeeStatusHistory request, CancellationToken cancellationToken)
        {
            var response = new EmployeeStatusHistoryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteEmployeeStatusHistory();

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
