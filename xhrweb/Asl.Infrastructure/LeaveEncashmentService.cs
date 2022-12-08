using Asl.Infrastructure.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Application.LeaveBalances;
using Attendance.Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asl.Infrastructure
{
    public class LeaveEncashmentService : ILeaveEncashmentService
    {
        public LeaveEncashmentService(IConfiguration configuration, IMediator mediator
            //,ILogger<PushNotificationService> logger
            )
        {
            _configuration = configuration;

            _mediator = mediator;
            //_logger = logger;
        }
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        //private readonly ILogger<PushNotificationService> _logger;
        public async Task<bool> AdjustLeaveBalanceAfterEncashment(Guid? CompanyId, Guid? EmployeeId, Guid LeaveTypeId, string LeaveCalendar, decimal? LeaveEncashedDays)
        {
            //var leaveBalanceQuery = new LeaveBalanceByTypeSpecification(LeaveTypeId, LeaveCalendar, EmployeeId.Value, false);

            var processingCommand = new LeaveBalanceCommands.V1.AdjustLeaveBalanceAfterEncashmentCommand
            {
                CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                EmployeeId = (EmployeeId != null && EmployeeId != Guid.Empty) ? EmployeeId.Value : Guid.Empty,
                LeaveCalendar = LeaveCalendar,
                LeaveTypeId = LeaveTypeId,
                LeaveEncashedDays = LeaveEncashedDays
            };
            await _mediator.Send(processingCommand);
            return true;
        }
    }
}
