using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskDetail.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteTaskDetail, TaskDetailCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<TaskEntities.TaskDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskDetail, Guid> _repository;

        public async Task<TaskDetailCommandVM>
            Handle(Commands.V1.MarkAsDeleteTaskDetail request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteTaskDetail();

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
