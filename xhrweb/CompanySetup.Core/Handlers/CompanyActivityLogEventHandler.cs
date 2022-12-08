using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Core.Handlers
{
    public class CompanyActivityLogEventHandler : ICommandHandler<CompanyActivityLogEvent>
    {
        public CompanyActivityLogEventHandler(IConfiguration configuration, IActivityLogService activityLogService)
        {
            _configuration = configuration;
            _activityLogService = activityLogService;
        }
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        public async Task Handle(CompanyActivityLogEvent command)
        {
            await _activityLogService.InsertActivity(command.LoginUserId,
                command.CurrentUserCompanyId,
                command.SystemKeyword,
                command.Comment);
        }
    }
}
