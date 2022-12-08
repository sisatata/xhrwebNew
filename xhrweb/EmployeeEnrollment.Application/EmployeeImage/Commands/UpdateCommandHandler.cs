using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeImage.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeImage, EmployeeImageCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeImage, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeImage, Guid> _repository;

        public async Task<EmployeeImageCommandVM>
            Handle(Commands.V1.UpdateEmployeeImage request, CancellationToken cancellationToken)
        {
            var response = new EmployeeImageCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.EmployeeImageId);
                entity.UpdateEmployeeImage(

                         request.EmployeeImageId,
                         request.EmployeeId,
                         request.FamilyMemberId,
                         request.Photo,
                         request.PhotoId,
                         request.CompanyId,
                         request.IsDeleted

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

