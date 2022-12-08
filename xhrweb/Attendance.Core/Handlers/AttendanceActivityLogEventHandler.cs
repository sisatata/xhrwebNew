using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Handlers
{
    public class AttendanceActivityLogEventHandler : ICommandHandler<AttendanceActivityLogEvent>
    {
        public AttendanceActivityLogEventHandler(IConfiguration configuration, IActivityLogService activityLogService)
        {
            _configuration = configuration;
            _activityLogService = activityLogService;
        }
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        public async Task Handle(AttendanceActivityLogEvent command)
        {
            await _activityLogService.InsertActivity(command.LoginUserId,
                command.CurrentUserCompanyId,
                command.SystemKeyword,
                command.Comment);
        }
    }
}
