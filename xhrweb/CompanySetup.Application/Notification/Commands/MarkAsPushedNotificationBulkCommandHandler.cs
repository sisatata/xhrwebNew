using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Notification.Commands
{
    public class MarkAsPushedNotificationBulkCommandHandler : IRequestHandler<Commands.V1.MarkAsPushedNotificationBulk, NotificationCommandVM>
    {
        public MarkAsPushedNotificationBulkCommandHandler(IAsyncRepository<CompanyEntities.Notification, Guid> repository,
            INotificationBulkRepository notificationBulkRepository)
        {
            _repository = repository;
            _notificationBulkRepository = notificationBulkRepository;
        }

        private readonly IAsyncRepository<CompanyEntities.Notification, Guid> _repository;
        private readonly INotificationBulkRepository _notificationBulkRepository;

        public async Task<NotificationCommandVM>
            Handle(Commands.V1.MarkAsPushedNotificationBulk request, CancellationToken cancellationToken)
        {
            var response = new NotificationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var notifications = new List<CompanyEntities.Notification>();

                foreach (var item in request.Ids)
                {
                    var entity = await _repository.GetByIdAsync(item);
                    entity.MarkAsPushedNotification();
                    notifications.Add(entity);
                }

                _notificationBulkRepository.InsertUpdate(notifications);


                response.Status = true;
                response.Message = "entity marked as viewed successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
