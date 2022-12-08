using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
using Attendance.Core.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RequestEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceRequest.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateAttendanceRequest, AttendanceRequestCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<RequestEntities.AttendanceRequest, Guid> repository,
            IAsyncRepository<AttendanceRequestApproveQueue, Guid> attendanceRequestApproveQueue,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
             IServiceBus serviceBus,
            IConfiguration configuration)
        {
            _repository = repository;
            _attendanceRequestApproveQueue = attendanceRequestApproveQueue;
            _employeeManagerRepository = employeeManagerRepository;
            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        private readonly IAsyncRepository<RequestEntities.AttendanceRequest, Guid> _repository;

        private readonly IAsyncRepository<RequestEntities.AttendanceRequestApproveQueue, Guid> _attendanceRequestApproveQueue;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        public async Task<AttendanceRequestCommandVM> Handle(Commands.V1.CreateAttendanceRequest request, CancellationToken cancellationToken)
        {
            var response = new AttendanceRequestCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = RequestEntities.AttendanceRequest.Create(
                         request.EmployeeId,
                         request.RequestTypeId,
                         request.StartTime,
                         request.EndTime,
                         request.Reason,
                         request.CompanyId,
                          ((int)ApprovalStatuses.Pending).ToString()

                );
                var data = await _repository.AddAsync(entity).ConfigureAwait(false);

                var attendanceManagerFilter = new EmployeeAttendanceManagerSpecificaion(request.EmployeeId);
                var attendanceManagers = await _employeeManagerRepository.ListAsync(attendanceManagerFilter).ConfigureAwait(false);

                if (attendanceManagers.Any())
                {
                    var attnManager = attendanceManagers.FirstOrDefault(x => x.ManagerTypeCode.ToLower() == "reporting");
                    AttendanceRequestApproveQueue oQueue = RequestEntities.AttendanceRequestApproveQueue.Create(data.Id, attnManager.ManagerId,
                        ((int)ApprovalStatuses.Pending).ToString()
                        , null, "");

                    var approvalQueue = await _attendanceRequestApproveQueue.AddAsync(oQueue).ConfigureAwait(false);
                    
                    // publish the event in message queue.
                    var @event = new Core.ApplicationEvents.AttendanceAppliedEvent
                    {
                        ManagerId = attnManager.ManagerId.Value,
                        EmployeeId = attnManager.EmployeeId,
                        RequestType = ((AttendanceRequestTypes)entity.RequestTypeId).ToDescription(),
                        StartTime = entity.StartTime.Value,
                        EndTime = entity.EndTime.Value,
                        ApplicationId = entity.Id,
                        CommandPublished = DateTime.Now
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }

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
