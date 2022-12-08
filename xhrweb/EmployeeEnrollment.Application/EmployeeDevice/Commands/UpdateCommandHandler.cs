using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeDevice.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeDevice, EmployeeDeviceCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeDevice, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeDevice, Guid> _repository;

        public async Task<EmployeeDeviceCommandVM>
            Handle(Commands.V1.UpdateEmployeeDevice request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDeviceCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeDevice(
                         request.AccessToken,
                         request.Location,
                         request.Device,
                         request.OperatingSystem,
                         request.OSVersion,
                         request.UserId,
                         request.EmployeeId

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

