using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateRawFileDetail, RawFileDetailCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.RawFileDetail, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.RawFileDetail, Guid> _repository;

        public async Task<RawFileDetailCommandVM>
            Handle(Commands.V1.UpdateRawFileDetail request, CancellationToken cancellationToken)
        {
            var response = new RawFileDetailCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateRawFileDetail(
                         request.FileName,
                         request.FileType,
                         request.Comments,
                         request.CompanyId

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

