using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class BillApprovedEventHandler : ICommandHandler<BillApprovedEvent>
    {
        public BillApprovedEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(BillApprovedEvent command)
        {
            await _pushNotificationService.SendBillApproveNotification(command.ManagerId, command.EmployeeId, command.BillAmount, command.ApprovedAmount,
                command.ApplicationId);

            await _pushNotificationService.SendBillApproveNotificationForPayment(command.AccountManagerId.Value, command.EmployeeId, command.ApprovedAmount,
                command.ApplicationId);
        }
    }
}
