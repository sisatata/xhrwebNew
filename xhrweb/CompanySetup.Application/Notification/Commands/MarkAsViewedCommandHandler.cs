using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Notification.Commands
{
    public class MarkAsViewedCommandHandler : IRequestHandler<Commands.V1.MarkAsViewedNotification, NotificationCommandVM>
    {
        public MarkAsViewedCommandHandler(IAsyncRepository<CompanyEntities.Notification, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Notification, Guid> _repository;

        public async Task<NotificationCommandVM>
            Handle(Commands.V1.MarkAsViewedNotification request, CancellationToken cancellationToken)
        {
            var response = new NotificationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsViewedNotification();

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity marked as viewed successfully.";
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
