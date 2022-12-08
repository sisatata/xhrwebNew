using CompanySetup.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using OfficeNoticeEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.OfficeNotice.Commands
{
    public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteOfficeNotice, OfficeNoticeCommandVM>
    {
        public MarkAsDeleteCommandHandler(IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<OfficeNoticeEntities.OfficeNotice, Guid> _repository;

        public async Task<OfficeNoticeCommandVM>
            Handle(Commands.V1.MarkAsDeleteOfficeNotice request, CancellationToken cancellationToken)
        {
            var response = new OfficeNoticeCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.MarkAsDeleteOfficeNotice();

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
