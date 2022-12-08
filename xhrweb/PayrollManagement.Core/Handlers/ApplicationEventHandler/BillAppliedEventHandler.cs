using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class BillAppliedEventHandler : ICommandHandler<BillAppliedEvent>
    {
        public BillAppliedEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(BillAppliedEvent command)
        {
            await _pushNotificationService.SendBillApplyNotification(command.ManagerId, command.EmployeeId, command.BillAmount, command.ApplicationId);
        }
    }
}
