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
    public class LeaveAppliedEventHandler : ICommandHandler<LeaveAppliedEvent>
    {
        public LeaveAppliedEventHandler(IConfiguration configuration, IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(LeaveAppliedEvent command)
        {
            //await _pushNotificationService.SendLeaveApplyNotification(command.ma, command.EmployeeName, command.NoOfDays, command.ApplicationId);
        }
    }
}
