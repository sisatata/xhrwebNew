using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Interfaces;

namespace TaskManagement.Application.TaskDetail.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateTaskDetail, TaskDetailCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<TaskEntities.TaskDetail, Guid>
        repository, ICurrentUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;

        }

        private readonly IAsyncRepository<TaskEntities.TaskDetail, Guid>
        
        _repository;
        private readonly ICurrentUserContext _userContext;

        public async Task<TaskDetailCommandVM> Handle(Commands.V1.CreateTaskDetail request, CancellationToken cancellationToken)
        {
            var response = new TaskDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
               
               // var uId = new Guid(_userContext.CurrentUserId);
                var entity = TaskEntities.TaskDetail.Create(
                         request.TaskTypeId,
                         request.TaskName,
                         request.TaskDescription,
                         request.StartDate,
                         request.EndDate,
                         request.AssigneeId,
                         request.CompanyId,
                         request.ParentTaskId,
                         request.Progress,
                         request.TaskCreator 
                         


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
