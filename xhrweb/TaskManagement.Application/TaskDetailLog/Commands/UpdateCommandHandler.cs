using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskDetailLog.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateTaskDetailLog, TaskDetailLogCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<TaskEntities.TaskDetailLog, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskDetailLog, Guid> _repository;

        public async Task<TaskDetailLogCommandVM>
            Handle(Commands.V1.UpdateTaskDetailLog request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailLogCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateTaskDetailLog(
                         request.TaskDetailId,
                         request.UpdateInfo,
                         request.DateUpdated,
                         request.EmployeeId,
                         request.StartDate,
                         request.EndDate,
                         request.SpendTime

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

