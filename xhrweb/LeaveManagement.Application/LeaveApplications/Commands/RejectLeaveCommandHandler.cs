using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Application.LeaveApplications.Commands.Models;
using LeaveManagement.Application.LeaveApplications.Commands.V1;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveApplications.Commands
{
    public class RejectLeaveCommandHandler : IRequestHandler<RejectLeaveCommand, LeaveApplicationVM>
    {
        public RejectLeaveCommandHandler(IAsyncRepository<LeaveApplication, Guid> leaveApplicationRepository,
            IAsyncRepository<LeaveBalance, Guid> leaveBalanceRepository,
            IReferenceRepository<EmployeeManager, Guid> employeeManagerRepository,
            IAsyncRepository<LeaveApplicationApproveQueue, Guid> leaveApplicationApproveQueue,
            IServiceBus serviceBus,
            IConfiguration configuration,
            IActivityLogService activityLogService)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeManagerRepository = employeeManagerRepository;
            _leaveApplicationApproveQueueRepository = leaveApplicationApproveQueue;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<LeaveApplication, Guid> _leaveApplicationRepository;
        private readonly IAsyncRepository<LeaveBalance, Guid> _leaveBalanceRepository;
        private readonly IReferenceRepository<EmployeeManager, Guid> _employeeManagerRepository;
        private readonly IAsyncRepository<LeaveApplicationApproveQueue, Guid> _leaveApplicationApproveQueueRepository;
        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        public async Task<LeaveApplicationVM> Handle(RejectLeaveCommand request, CancellationToken cancellationToken)
        {
            var response = new LeaveApplicationVM
            {
                Message = LeaveConstants.Error,
                Status = false
            };

            if (request == null) return response;

            try
            {
                var leaveApplication = await _leaveApplicationRepository.GetByIdAsync(request.ApplicationId).ConfigureAwait(false);

                var leaveApplicationAggregate = new LeaveApplicationAggregate(leaveApplication);
                leaveApplicationAggregate.RejectLeaveApplication(request.Notes);

                await _leaveApplicationRepository.UpdateAsync(leaveApplicationAggregate.LeaveApplication).ConfigureAwait(false);

                //var leaveBalanceFilter = new LeaveBalanceByTypeSpecification(leaveApplication.LeaveTypeId, leaveApplication.LeaveCalendar, leaveApplication.EmployeeId, false);

                //var leaveBalanceQuery = await _leaveBalanceRepository.ListAsync(leaveBalanceFilter).ConfigureAwait(false);

                //if (!leaveBalanceQuery.Any()) return response;
                //var leaveBalance = leaveBalanceQuery.FirstOrDefault();
                //leaveBalance.AdjustBalance(leaveApplication.NoOfDays);
                //var leaveBalanceToUpdate = await _leaveBalanceRepository.GetByIdAsync(leaveBalance.Id).ConfigureAwait(false);
                //leaveBalanceToUpdate.AdjustBalance(-leaveApplication.NoOfDays);

                //await _leaveBalanceRepository.UpdateAsync(leaveBalanceToUpdate).ConfigureAwait(false);

                var leaveManagerFilter = new EmployeeLeaveManagerSpecificaion(leaveApplication.EmployeeId);
                var leaveManagers = await _employeeManagerRepository.ListAsync(leaveManagerFilter).ConfigureAwait(false);

                if (leaveManagers.Any())
                {
                    var leaveManager = leaveManagers.FirstOrDefault();
                    var leaveQueueFilter = new LeaveApplicationApproveQueueFilterSpecification(leaveApplication.Id, leaveManager.ManagerId.Value);

                    var leaveQueues = await _leaveApplicationApproveQueueRepository.ListAsync(leaveQueueFilter).ConfigureAwait(false);
                    var leaveQueue = leaveQueues.FirstOrDefault();
                    leaveQueue.Note = request.Notes;
                    leaveQueue.ApprovalStatus = leaveApplication.ApprovalStatus;
                    leaveQueue.ApprovedTime = DateTime.Now;
                    await _leaveApplicationApproveQueueRepository.UpdateAsync(leaveQueue).ConfigureAwait(false);
                    // publish the event in message queue.

                    var @event = new Core.ApplicationEvents.LeaveDeclinedEvent
                    {
                        ManagerId = leaveManager.ManagerId.Value,
                        EmployeeId = leaveApplication.EmployeeId,
                        ApplicationId = leaveApplication.Id,
                        NoOfDays = leaveApplication.NoOfDays,
                        CommandPublished = DateTime.Now,
                        StartDate = leaveApplication.StartDate,
                        EndDate = leaveApplication.EndDate,
                        Note = request.Notes,
                        IsHalfDayLeave = leaveApplication.IsHalfDayLeave
                    };

                    await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                }

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
