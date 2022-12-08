using ASL.Hrms.SharedKernel.Enums;
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
using AttendanceEntities = Attendance.Core.Entities;

namespace Attendance.Application.AttendanceRequest.Commands
{
    public class ApproveAttendanceRequestHandler : IRequestHandler<Commands.V1.ApproveAttendanceRequest, AttendanceRequestCommandVM>
    {
        public ApproveAttendanceRequestHandler(IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> repository,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
            IAsyncRepository<AttendanceRequestApproveQueue, Guid> attendanceRequestApproveQueueRepository,
             IServiceBus serviceBus,
            IConfiguration configuration, IMediator mediator)
        {
            _repository = repository;
            _employeeManagerRepository = employeeManagerRepository;
            _attendanceRequestApproveQueueRepository = attendanceRequestApproveQueueRepository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<AttendanceEntities.AttendanceRequest, Guid> _repository;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IAsyncRepository<AttendanceRequestApproveQueue, Guid> _attendanceRequestApproveQueueRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public async Task<AttendanceRequestCommandVM>
            Handle(Commands.V1.ApproveAttendanceRequest request, CancellationToken cancellationToken)
        {
            var response = new AttendanceRequestCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.ApproveAttendanceRequest(
                         //request.EmployeeId,
                         //request.RequestTypeId,
                         //request.StartTime,
                         //request.EndTime,
                         //request.Reason,
                         //request.AprovalStatus,
                         request.Note
                         //request.CompanyId

    );

                await _repository.UpdateAsync(entity);

                var attnManagerFilter = new EmployeeAttendanceManagerSpecificaion(entity.EmployeeId);
                var managers = await _employeeManagerRepository.ListAsync(attnManagerFilter).ConfigureAwait(false);

                if (managers.Any())
                {
                    var Manager = managers.FirstOrDefault(x => x.ManagerTypeCode.ToLower() == "reporting");
                    var QueueFilter = new AttendanceRequestApproveQueueFilterSpecification(request.Id, Manager.ManagerId.Value);

                    var Queues = await _attendanceRequestApproveQueueRepository.ListAsync(QueueFilter).ConfigureAwait(false);
                    var Queue = Queues.FirstOrDefault();
                    Queue.Note = request.Note;
                    Queue.ApprovalStatus = entity.ApprovalStatus;
                    Queue.ApprovedDate = DateTime.Now;
                    await _attendanceRequestApproveQueueRepository.UpdateAsync(Queue).ConfigureAwait(false);

                    var processingCommand = new AttendanceProcessedData.Commands.Commands.V1.AttendanceProcessDataCommand
                    {
                        CompanyId = entity.CompanyId,
                        EmployeeId = entity.EmployeeId,
                        ProcessingStartDate = entity.StartTime.Value.Date,
                        ProcessingEndDate = entity.StartTime.Value.Date
                    };
                    await _mediator.Send(processingCommand);

                    // publish the event in message queue.
                    var @event = new Core.ApplicationEvents.AttendanceApprovedEvent
                    {
                        ManagerId = Manager.ManagerId.Value,
                        EmployeeId = Manager.EmployeeId,
                        RequestType = ((AttendanceRequestTypes)entity.RequestTypeId).ToString(),
                        StartTime = entity.StartTime.Value,
                        EndTime = entity.EndTime.Value,
                        ApplicationId = entity.Id,
                        CommandPublished = DateTime.Now
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }

                response.Status = true;
                response.Message = "entity approved successfully.";
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

