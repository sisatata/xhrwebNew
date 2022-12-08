using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class BillDeclinedEventHandler : ICommandHandler<BillDeclinedEvent>
    {
        public BillDeclinedEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(BillDeclinedEvent command)
        {
            await _pushNotificationService.SendBillDeclineNotification(command.ManagerId, command.EmployeeId, command.ApplicationId);
        }
    }
}
