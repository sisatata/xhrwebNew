using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskDetailLog.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateTaskDetailLog, TaskDetailLogCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<TaskEntities.TaskDetailLog, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskDetailLog, Guid>
        _repository;

        public async Task<TaskDetailLogCommandVM> Handle(Commands.V1.CreateTaskDetailLog request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailLogCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = TaskEntities.TaskDetailLog.Create(
                         request.TaskDetailId,
                         request.UpdateInfo,
                         request.DateUpdated,
                         request.EmployeeId,
                         request.StartDate,
                         request.EndDate,
                         request.SpendTime
                );
                var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
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
