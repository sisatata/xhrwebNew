using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Notification.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateNotification, NotificationCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<CompanyEntities.Notification, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<CompanyEntities.Notification, Guid> _repository;

        public async Task<NotificationCommandVM> Handle(Commands.V1.CreateNotification request, CancellationToken cancellationToken)
        {
            var response = new NotificationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = CompanyEntities.Notification.Create(
                         request.ApplicationId,
                         request.ApplicantId,
                         request.ManagerId,
                         request.NotificationTitle,
                         request.NotificationDetail,
                         request.IsViewed,
                         request.ViewedTime,
                         request.NotificationTypeId,
                         request.NotificationOwnerId,
                         request.IsPushed,
                         request.PushedTime
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
