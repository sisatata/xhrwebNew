using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ActivityLog.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteActivityLog, ActivityLogCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<CompanyEntities.ActivityLog, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.ActivityLog, Guid> _repository;

        public async Task<ActivityLogCommandVM>
            Handle(Commands.V1.MarkAsDeleteActivityLog request, CancellationToken cancellationToken)
        {
            var response = new ActivityLogCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteActivityLog();

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
