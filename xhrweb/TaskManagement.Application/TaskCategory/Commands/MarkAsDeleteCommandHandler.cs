using TaskManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskEntities = TaskManagement.Core.Entities;

namespace TaskManagement.Application.TaskCategory.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteTaskCategory, TaskCategoryCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<TaskEntities.TaskCategory, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<TaskEntities.TaskCategory, Guid> _repository;

        public async Task<TaskCategoryCommandVM>
            Handle(Commands.V1.MarkAsDeleteTaskCategory request, CancellationToken cancellationToken)
        {
            var response = new TaskCategoryCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteTaskCategory();

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
