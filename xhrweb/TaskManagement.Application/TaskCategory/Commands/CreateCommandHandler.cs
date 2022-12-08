using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskCategory.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateTaskCategory, TaskCategoryCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<TaskEntities.TaskCategory, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskCategory, Guid>
        _repository;

        public async Task<TaskCategoryCommandVM> Handle(Commands.V1.CreateTaskCategory request, CancellationToken cancellationToken)
        {
            var response = new TaskCategoryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = TaskEntities.TaskCategory.Create(

                         request.TaskCategoryName,
                         request.Remarks,
                         request.CompanyId



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
