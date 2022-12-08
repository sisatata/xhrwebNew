using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class LeaveEncashmentEventHandler : ICommandHandler<LeaveEncashmentEvent>
    {
        public LeaveEncashmentEventHandler(IConfiguration configuration,
            ILeaveEncashmentService leaveEncashmentService)
        {
            _configuration = configuration;
            _leaveEncashmentService = leaveEncashmentService;
        }
        private readonly IConfiguration _configuration;
        private readonly ILeaveEncashmentService _leaveEncashmentService;
        public async Task Handle(LeaveEncashmentEvent command)
        {
            await _leaveEncashmentService.AdjustLeaveBalanceAfterEncashment(command.CompanyId, command.EmployeeId, command.LeaveTypeId, command.LeaveCalendar,command.LeaveEncashedDays);
        }
    }
}
