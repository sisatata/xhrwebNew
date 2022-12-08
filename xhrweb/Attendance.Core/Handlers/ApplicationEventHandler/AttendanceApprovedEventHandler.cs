using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.ApplicationEvents;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Handlers.ApplicationEventHandler
{
    public class AttendanceApprovedEventHandler : ICommandHandler<AttendanceApprovedEvent>
    {
        public AttendanceApprovedEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(AttendanceApprovedEvent command)
        {
            await _pushNotificationService.SendAttendanceApproveNotification(command.ManagerId, command.EmployeeId, command.RequestType,command.StartTime,
               command.EndTime, command.ApplicationId);
        }
    }
}
