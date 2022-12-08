using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskCategory.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateTaskCategory, TaskCategoryCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<TaskEntities.TaskCategory, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskCategory, Guid> _repository;

        public async Task<TaskCategoryCommandVM>
            Handle(Commands.V1.UpdateTaskCategory request, CancellationToken cancellationToken)
        {
            var response = new TaskCategoryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateTaskCategory(

                         request.TaskCategoryName,
                         request.Remarks,
                         request.CompanyId

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

