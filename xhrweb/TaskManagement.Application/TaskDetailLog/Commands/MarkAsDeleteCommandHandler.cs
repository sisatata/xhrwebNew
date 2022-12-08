using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskDetailLog.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteTaskDetailLog, TaskDetailLogCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<TaskEntities.TaskDetailLog, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskDetailLog, Guid> _repository;

        public async Task<TaskDetailLogCommandVM>
            Handle(Commands.V1.MarkAsDeleteTaskDetailLog request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailLogCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteTaskDetailLog();

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
