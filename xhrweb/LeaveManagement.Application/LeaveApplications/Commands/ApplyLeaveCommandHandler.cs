using LeaveManagement.Application.LeaveApplications.Commands.Models;
using LeaveManagement.Application.LeaveApplications.Commands.V1;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using LeaveManagement.Core.Specifications;
using System.Linq;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using LeaveManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LeaveManagement.Application.LeaveApplications.Commands
{
    public class ApplyLeaveCommandHandler : IRequestHandler<ApplyLeaveCommand, LeaveApplicationVM>
    {
        public ApplyLeaveCommandHandler(IAsyncRepository<LeaveType, Guid> leaveTypeRepository,
            IAsyncRepository<LeaveBalance, Guid> leaveBalanceRepository,
            IAsyncRepository<LeaveApplication, Guid> leaveApplicationRepository,
            IAsyncRepository<LeaveApplicationApproveQueue, Guid> leaveApplicationApproveQueue,
            IReferenceRepository<LeaveCalendar, Guid> leaveCalendarRepository,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
            IServiceBus serviceBus,
            IConfiguration configuration,
            IActivityLogService activityLogService)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveApplicationApproveQueueRepository = leaveApplicationApproveQueue;

            _leaveCalendarRepository = leaveCalendarRepository;
            _employeeManagerRepository = employeeManagerRepository;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<LeaveType, Guid> _leaveTypeRepository;
        private readonly IAsyncRepository<LeaveBalance, Guid> _leaveBalanceRepository;
        private readonly IAsyncRepository<LeaveApplication, Guid> _leaveApplicationRepository;
        private readonly IAsyncRepository<LeaveApplicationApproveQueue, Guid> _leaveApplicationApproveQueueRepository;

        private readonly IReferenceRepository<LeaveCalendar, Guid> _leaveCalendarRepository;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;

        public async Task<LeaveApplicationVM> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
        {
            var response = new LeaveApplicationVM
            {
                Message = LeaveConstants.Error,
                Status = false
            };

            if (request == null) return response;

            try
            {
                var leaveCalenderFilter = new LeaveCalenderFilterSpecification(request.CompanyId, request.StartDate.Date);
                var leaveCalendar = await _leaveCalendarRepository.ListAsync(leaveCalenderFilter).ConfigureAwait(false);
                if (!leaveCalendar.Any())
                    return response;
                else
                    request.LeaveCalendar = leaveCalendar.FirstOrDefault().Year;

                var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId).ConfigureAwait(false);
                var leaveBalanceFilter = new LeaveBalanceByTypeSpecification(request.LeaveTypeId, request.LeaveCalendar, request.EmployeeId, false);
                var leaveBalanceQuery = await _leaveBalanceRepository.ListAsync(leaveBalanceFilter).ConfigureAwait(false);

                if (!leaveBalanceQuery.Any()) return response;
                var leaveBalance = leaveBalanceQuery.FirstOrDefault();

                var employeeLeaveApplicationDataFilter = new EmployeeLeaveApplicationDataSpecificaion(request.EmployeeId, request.LeaveCalendar);
                var employeeLeaveApplicationData = await _leaveApplicationRepository.ListAsync(employeeLeaveApplicationDataFilter).ConfigureAwait(false);

                var leaveApplicationggregate = new LeaveApplicationAggregate(request.CompanyId, request.EmployeeId, leaveType, leaveBalance, employeeLeaveApplicationData);
                leaveApplicationggregate.NewLeaveApplication(request.StartDate, request.EndDate, request.LeaveDays, request.LeaveCalendar, request.Reason, request.IsHalfDayLeave, request.HalfDayLeaveTypeId,
                    request.AddressOnLeave, request.EmergencyContact);

                var application = await _leaveApplicationRepository.AddAsync(leaveApplicationggregate.LeaveApplication).ConfigureAwait(false);
                var leaveManagerFilter = new EmployeeLeaveManagerSpecificaion(request.EmployeeId);
                var leaveManagers = await _employeeManagerRepository.ListAsync(leaveManagerFilter).ConfigureAwait(false);

                if (leaveManagers.Any())
                {
                    var leaveManager = leaveManagers.FirstOrDefault(x => x.ManagerTypeCode.ToLower() == "reporting");
                    leaveApplicationggregate.LeaveApplicationApproveQueue.LeaveApplicationId = application.Id;
                    leaveApplicationggregate.LeaveApplicationApproveQueue.ManagerId = leaveManager.ManagerId;
                    leaveApplicationggregate.LeaveApplicationApproveQueue.ApprovalStatus = ((int)ApprovalStatuses.Pending).ToString();
                    var approvalQueue = await _leaveApplicationApproveQueueRepository.AddAsync(leaveApplicationggregate.LeaveApplicationApproveQueue).ConfigureAwait(false);

                    // publish the event in message queue.

                    var @event = new Core.ApplicationEvents.LeaveNotificationEvent
                    {
                        ManagerId = leaveManager.ManagerId.Value,
                        EmployeeId = application.EmployeeId,
                        ApplicationId = application.Id,
                        NoOfDays = application.NoOfDays,
                        CommandPublished = DateTime.Now,

                        StartDate= application.StartDate,
                        EndDate= application.EndDate,
                        Reason = application.Reason,
                        IsHalfDayLeave = application.IsHalfDayLeave
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }
                response.ApplicationId = application.Id;
                response.Status = true;
                response.Message = LeaveConstants.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
