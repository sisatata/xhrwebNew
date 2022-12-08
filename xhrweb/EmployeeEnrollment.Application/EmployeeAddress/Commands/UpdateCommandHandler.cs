using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeAddress.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeAddress, EmployeeAddressCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeAddress, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeAddress, Guid> _repository;

        public async Task<EmployeeAddressCommandVM>
            Handle(Commands.V1.UpdateEmployeeAddress request, CancellationToken cancellationToken)
        {
            var response = new EmployeeAddressCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.EmployeeAddressId);
                entity.UpdateEmployeeAddress(

                         //request.EmployeeAddressId,
                         request.EmployeeId,
                         request.AddressLine1,
                         request.AddressLine2,
                         request.Village,
                         request.PostOffice,
                         request.ThanaId,
                         request.DistrictId,
                         request.CountryId,
                         request.Latitude,
                         request.Longitude,
                         request.AddressTypeId,
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

