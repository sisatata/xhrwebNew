using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Events;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Handlers
{
    public class HolidayPushNotificationProcessEventHandler : ICommandHandler<HolidayPushNotificationProcessEvent>
    {
        public HolidayPushNotificationProcessEventHandler(IConfiguration configuration,
            IPushNotificationProcessService pushNotificationProcessService)
        {
            _configuration = configuration;
            _pushNotificationProcessService = pushNotificationProcessService;
        }

        private readonly IConfiguration _configuration;
        private readonly IPushNotificationProcessService _pushNotificationProcessService;
        public async Task Handle(HolidayPushNotificationProcessEvent command)
        {
           await _pushNotificationProcessService.HolidayPushNotificationProcess(command.CompanyId.Value, command.HolidayDate);
        }
    }
}
