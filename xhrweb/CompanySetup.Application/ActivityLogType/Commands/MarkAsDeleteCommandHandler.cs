using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ActivityLogType.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteActivityLogType, ActivityLogTypeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<CompanyEntities.ActivityLogType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.ActivityLogType, Guid> _repository;

        public async Task<ActivityLogTypeCommandVM>
            Handle(Commands.V1.MarkAsDeleteActivityLogType request, CancellationToken cancellationToken)
        {
            var response = new ActivityLogTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteActivityLogType();

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
