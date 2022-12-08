using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Application.Notification.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CompanySetup.Application.Notification.Commands.Commands.V1;

namespace Asl.Infrastructure
{
    public class PushNotificationProcessService : IPushNotificationProcessService
    {
        public PushNotificationProcessService(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public async Task HolidayPushNotificationProcess(Guid companyId, DateTime holidayDate)
        {
            var holidayPushNotificationCommand = new ProcessHolidayNotificationBulk
            {
                CompanyId = companyId,
                ProcessDate = holidayDate
            };
           await _mediator.Send(holidayPushNotificationCommand);
        }
    }
}
