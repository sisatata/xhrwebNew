using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;
using System.Linq;

namespace CompanySetup.Application.ActivityLog.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateActivityLog, ActivityLogCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<CompanyEntities.ActivityLog, Guid> repository,
            IAsyncRepository<CompanyEntities.ActivityLogType, Guid> repositoryLogType)
        {
            _repository = repository;
            _repositoryLogType = repositoryLogType;

        }

        private readonly IAsyncRepository<CompanyEntities.ActivityLog, Guid> _repository;
        private readonly IAsyncRepository<CompanyEntities.ActivityLogType, Guid> _repositoryLogType;

        public async Task<ActivityLogCommandVM>
            Handle(Commands.V1.UpdateActivityLog request, CancellationToken cancellationToken)
        {
            var response = new ActivityLogCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var activityLogTypeBySystemNameFilter = new ActivityLogTypeBySystemNameFilterSpecification(request.SystemKeyword);
                var activityLogTypes = await _repositoryLogType.ListAsync(activityLogTypeBySystemNameFilter).ConfigureAwait(false);

                if (!activityLogTypes.Any())
                {
                    response.Message = "Na active activity log type";
                    return response;
                }

                var activityLogType = activityLogTypes.FirstOrDefault();

                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateActivityLog(
                    request.CompanyId,
                         activityLogType.Id,
                         request.UserId,
                         request.Comment,
                         request.IpAddress,
                         request.EntityId

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

