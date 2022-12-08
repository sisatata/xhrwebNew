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
using ASL.Hrms.SharedKernel.Interfaces;

namespace LeaveManagement.Application.LeaveApplications.Commands
{
    public class UpdateLeaveCommandHandler : IRequestHandler<UpdateLeaveCommand, LeaveApplicationVM>
    {
        public UpdateLeaveCommandHandler(IAsyncRepository<LeaveType, Guid> leaveTypeRepository,
            IAsyncRepository<LeaveBalance, Guid> leaveBalanceRepository,
            IAsyncRepository<LeaveApplication, Guid> leaveApplicationRepository,
            IReferenceRepository<LeaveCalendar, Guid> leaveCalendarRepository,
            IActivityLogService activityLogService)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _leaveApplicationRepository = leaveApplicationRepository;
            _leaveCalendarRepository = leaveCalendarRepository;
            _activityLogService = activityLogService;
        }

        private readonly IAsyncRepository<LeaveType, Guid> _leaveTypeRepository;
        private readonly IAsyncRepository<LeaveBalance, Guid> _leaveBalanceRepository;
        private readonly IAsyncRepository<LeaveApplication, Guid> _leaveApplicationRepository;
        private readonly IReferenceRepository<LeaveCalendar, Guid> _leaveCalendarRepository;
        private readonly IActivityLogService _activityLogService;

        public async Task<LeaveApplicationVM> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
        {
            var response = new LeaveApplicationVM
            {
                Message = LeaveConstants.Error,
                Status = false
            };

            if (request == null) return response;

            try
            {
                var leaveCalenderFilter = new LeaveCalenderFilterSpecification(request.CompanyId, request.StartDate);
                var leaveCalendar = await _leaveCalendarRepository.ListAsync(leaveCalenderFilter).ConfigureAwait(false);
                if (leaveCalendar == null)
                    return response;
                else
                    request.LeaveCalendar = leaveCalendar.FirstOrDefault().Year;

                var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId).ConfigureAwait(false);
                var leaveBalanceFilter = new LeaveBalanceByTypeSpecification(request.LeaveTypeId, request.LeaveCalendar, request.EmployeeId, false);
                var leaveBalanceQuery = await _leaveBalanceRepository.ListAsync(leaveBalanceFilter).ConfigureAwait(false);

                if (!leaveBalanceQuery.Any()) return response;
                var leaveBalance = leaveBalanceQuery.FirstOrDefault();
                var leaveApplicationDB = await _leaveApplicationRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

                var employeeLeaveApplicationDataFilter = new EmployeeLeaveApplicationDataSpecificaion(request.EmployeeId, request.LeaveCalendar);
                var employeeLeaveApplicationData = await _leaveApplicationRepository.ListAsync(employeeLeaveApplicationDataFilter).ConfigureAwait(false);


                var leaveApplicationggregate = new LeaveApplicationAggregate(request.CompanyId, request.EmployeeId, leaveType, leaveBalance, employeeLeaveApplicationData);
                leaveApplicationggregate.UpdateLeaveApplication(leaveApplicationDB, request.StartDate, request.EndDate, request.LeaveDays,
                    request.LeaveCalendar, request.Reason, request.IsHalfDayLeave, request.HalfDayLeaveTypeId,
                    request.AddressOnLeave, request.EmergencyContact);

                await _leaveApplicationRepository.UpdateAsync(leaveApplicationggregate.LeaveApplication).ConfigureAwait(false);

                response.ApplicationId = request.Id;
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
