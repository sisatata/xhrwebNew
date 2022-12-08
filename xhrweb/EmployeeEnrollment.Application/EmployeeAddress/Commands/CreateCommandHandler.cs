using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeAddress.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeAddress, EmployeeAddressCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeAddress, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeAddress, Guid>
        _repository;

        public async Task<EmployeeAddressCommandVM> Handle(Commands.V1.CreateEmployeeAddress request, CancellationToken cancellationToken)
        {
            var response = new EmployeeAddressCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity =  EmployeeEntities.EmployeeAddress.Create(
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
