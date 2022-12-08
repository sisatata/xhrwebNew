using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Core.ApplicationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Handlers.ApplicationEventHandler
{
    public class BillPaidEventHandler : ICommandHandler<BillPaidEvent>
    {
        public BillPaidEventHandler(IConfiguration configuration,
            IPushNotificationService pushNotificationService)
        {
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public async Task Handle(BillPaidEvent command)
        {
            await _pushNotificationService.SendBillMarkPaidNotificationForApplicant(command.AccountManagerId, command.EmployeeId, command.PaidAmount,
                command.ApplicationId);
        }
    }
}
