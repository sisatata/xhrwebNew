using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.ApplicationEvents;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Handlers.ApplicationEventHandler
{
    public class AttendanceDeclinedEventHandler : ICommandHandler<AttendanceDeclinedEvent>
    {
        public AttendanceDeclinedEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(AttendanceDeclinedEvent command)
        {
            await _pushNotificationService.SendAttendanceDeclineNotification(command.ManagerId, command.EmployeeId, command.RequestType, command.StartTime,
               command.EndTime, command.ApplicationId);
        }
    }
}
