using ASL.Hrms.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CompanySetup.Application.Notification.Commands.Commands.V1;

namespace ASL.Hrms.Api.HostedServices
{
    public class ProcessPushNotificationHostedJob : CronJobService
    {
        private readonly ILogger<ProcessPushNotificationHostedJob> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        //private readonly IPushNotificationService _pushNotificationService;
        public ProcessPushNotificationHostedJob(IScheduleConfig<ProcessPushNotificationHostedJob> config, ILogger<ProcessPushNotificationHostedJob> logger, IMediator mediator, IConfiguration configuration)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
            //_pushNotificationService = pushNotificationService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessPushNotificationHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessPushNotificationHostedJob is starting.");
            try
            {
                var command = new ProcessHolidayNotificationBulk
                {
                    ProcessDate = DateTime.Now.AddDays(2).Date
                };

                var data = await _mediator.Send(command);

                var officeNoticeCommand = new ProcessOfficeNoticeNotificationBulk
                {
                    ProcessDate = DateTime.Now.Date
                };

                data = await _mediator.Send(officeNoticeCommand);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:hh:mm:ss} ProcessPushNotificationHostedJob got exception. {ex.Message}");
            }
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessPushNotificationHostedJob is ended.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessPushNotificationHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
