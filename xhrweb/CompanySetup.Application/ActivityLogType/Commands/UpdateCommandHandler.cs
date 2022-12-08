using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanySetupEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.ActivityLogType.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateActivityLogType, ActivityLogTypeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanySetupEntities.ActivityLogType, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanySetupEntities.ActivityLogType, Guid> _repository;

        public async Task<ActivityLogTypeCommandVM>
            Handle(Commands.V1.UpdateActivityLogType request, CancellationToken cancellationToken)
        {
            var response = new ActivityLogTypeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateActivityLogType(

                         request.SystemKeyword,
                         request.Name,
                         request.Enabled

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

