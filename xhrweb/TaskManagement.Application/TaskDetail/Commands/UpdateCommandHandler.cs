using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskDetail.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateTaskDetail, TaskDetailCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<TaskEntities.TaskDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskDetail, Guid> _repository;

        public async Task<TaskDetailCommandVM>
            Handle(Commands.V1.UpdateTaskDetail request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateTaskDetail(
                         request.TaskTypeId,
                         request.TaskName,
                         request.TaskDescription,
                         request.StartDate,
                         request.EndDate,
                         request.AssigneeId,
                         request.CompanyId,
                         request.ParentTaskId,
                         request.StatusId,
                         request.Progress
                         

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

