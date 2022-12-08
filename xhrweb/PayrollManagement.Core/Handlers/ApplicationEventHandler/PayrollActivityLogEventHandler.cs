using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class PayrollActivityLogEventHandler : ICommandHandler<PayrollActivityLogEvent>
    {
        public PayrollActivityLogEventHandler(IConfiguration configuration, IActivityLogService activityLogService)
        {
            _configuration = configuration;
            _activityLogService = activityLogService;
        }
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        public async Task Handle(PayrollActivityLogEvent command)
        {
            await _activityLogService.InsertActivity(command.LoginUserId, 
                command.CurrentUserCompanyId,
                command.SystemKeyword, 
                command.Comment);
        }
    }
}
