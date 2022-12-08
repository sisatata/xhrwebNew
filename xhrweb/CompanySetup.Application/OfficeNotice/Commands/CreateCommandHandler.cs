using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static CompanySetup.Application.Notification.Commands.Commands.V1;
using OfficeNoticeEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.OfficeNotice.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateOfficeNotice, OfficeNoticeCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> _repository;
        private readonly IMediator _mediator;

        public async Task<OfficeNoticeCommandVM> Handle(Commands.V1.CreateOfficeNotice request, CancellationToken cancellationToken)
        {
            var response = new OfficeNoticeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = OfficeNoticeEntities.OfficeNotice.Create(
                         request.CompanyId,
                         request.Subject,
                         request.Details,
                         request.IsGeneral,
                         request.IsSectionSpecific,
                         request.ApplicableSections,
                         request.PublishDate,
                         request.StartDate,
                         request.EndDate,
                         request.IsDeleted,
                         request.IsPublished
                );
                var data = await _repository.AddAsync(entity).ConfigureAwait(false);

                var pushNotification = new ProcessOfficeNoticeNotificationBulk
                {
                    ProcessDate = DateTime.Now.Date,
                    CompanyId = request.CompanyId
                };
                var d = await _mediator.Send(pushNotification).ConfigureAwait(false);

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
