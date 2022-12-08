using ASL.Hrms.SharedKernel.Interfaces;
using AutoMapper.Configuration;
using LeaveManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeEnrollment.Application.EmployeeDevice.Queries;
using MediatR;

namespace ASL.Hrms.Api.Hubs
{
    public class LeaveApprovedEventHandler //: ICommandHandler<LeaveAppliedEvent>
    {
        public LeaveApprovedEventHandler(IConfiguration configuration, IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(LeaveAppliedEvent command)
        {
            //await _pushNotificationService.SendLeaveApproveNotification(command.mana command.EmployeeId,  command.NoOfDays, command.ApplicationId);
        }
    }
}
