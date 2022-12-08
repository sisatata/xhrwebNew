using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Core.EventHandler
{
    public class EmployeeActivityLogEventHandler : ICommandHandler<EmployeeActivityLogEvent>
    {
        public EmployeeActivityLogEventHandler(IConfiguration configuration, IActivityLogService activityLogService)
        {
            _configuration = configuration;
            _activityLogService = activityLogService;
        }
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        public async Task Handle(EmployeeActivityLogEvent command)
        {
            await _activityLogService.InsertActivity(command.LoginUserId,
                command.CurrentUserCompanyId,
                command.SystemKeyword,
                command.Comment);
        }
    }
}
