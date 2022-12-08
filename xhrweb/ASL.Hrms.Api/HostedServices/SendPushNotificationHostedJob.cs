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
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;

namespace ASL.Hrms.Api.HostedServices
{
    public class SendPushNotificationHostedJob : CronJobService
    {
        private readonly ILogger<SendPushNotificationHostedJob> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationService _pushNotificationService;
        public SendPushNotificationHostedJob(IScheduleConfig<SendPushNotificationHostedJob> config, ILogger<SendPushNotificationHostedJob> logger, IMediator mediator, IConfiguration configuration,
            IPushNotificationService pushNotificationService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
            _pushNotificationService = pushNotificationService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SendPushNotificationHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} SendPushNotificationHostedJob is satrting.");
            try
            {
                _pushNotificationService.SendPushNotificationBulk();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:hh:mm:ss} SendPushNotificationHostedJob got exception. {ex.Message}");
            }
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} SendPushNotificationHostedJob is ended.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DatabaseBackupHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
