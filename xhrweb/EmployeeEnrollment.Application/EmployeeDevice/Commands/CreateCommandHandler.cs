using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using System.Linq;
using ASL.Hrms.SharedKernel.Enums;

namespace EmployeeEnrollment.Application.EmployeeDevice.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeDevice, EmployeeDeviceCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeDevice, Guid>
        repository, IEmployeeDevicesRepository employeeDevicesRepository)
        {
            _repository = repository;
            _employeeDevicesRepository = employeeDevicesRepository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeDevice, Guid> _repository;
        private IEmployeeDevicesRepository _employeeDevicesRepository;

        public async Task<EmployeeDeviceCommandVM> Handle(Commands.V1.CreateEmployeeDevice request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDeviceCommandVM
            {
                Status = false,
                Message = "error"
            };
            var employeeDeviceQuery = new EmployeeDeviceFilterSpecification(request.EmployeeId.Value, request.AccessToken);

            var employeeDeviceList = await _repository.ListAsync(employeeDeviceQuery).ConfigureAwait(false);
            //if (employeeDeviceList.Any())
            //{
            //    response.Message = "AccessToken already exists";
            //    return response;
            //}

            try
            {
                var entity = EmployeeEntities.EmployeeDevice.Create(
                         request.AccessToken,
                         request.Location,
                         request.Device,
                         request.OperatingSystem,
                         request.OSVersion,
                         request.UserId,
                         request.EmployeeId
                );
                var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = entity.Id;
                // delete old devices record
                if (employeeDeviceList.Any())
                {
                    foreach (var item in employeeDeviceList)
                    {
                        item.State = TrackingState.Deleted;
                    }
                    await _employeeDevicesRepository.Update(employeeDeviceList.ToList());
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
