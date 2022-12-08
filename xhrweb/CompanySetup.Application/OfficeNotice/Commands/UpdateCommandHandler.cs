using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using OfficeNoticeEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.OfficeNotice.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateOfficeNotice, OfficeNoticeCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> _repository;

        public async Task<OfficeNoticeCommandVM>
            Handle(Commands.V1.UpdateOfficeNotice request, CancellationToken cancellationToken)
        {
            var response = new OfficeNoticeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateOfficeNotice(
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

