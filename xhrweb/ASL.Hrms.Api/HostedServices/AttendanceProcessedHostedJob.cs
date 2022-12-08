using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;

namespace ASL.Hrms.Api.HostedServices
{
    public class AttendanceProcessedHostedJob : CronJobService
    {
        private readonly ILogger<AttendanceProcessedHostedJob> _logger;
        private readonly IMediator _mediator;
        public AttendanceProcessedHostedJob(IScheduleConfig<AttendanceProcessedHostedJob> config, ILogger<AttendanceProcessedHostedJob> logger, IMediator mediator)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AttendanceProcessedHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} AttendanceProcessedHostedJob is satrting.");
            var attendanceBackgroundProcessJob = new AttendanceProcessDataHangfireBackgroundCommand();
            attendanceBackgroundProcessJob.ProcessingStartDate = DateTime.Now.Date;
            attendanceBackgroundProcessJob.ProcessingEndDate = DateTime.Now.Date.AddHours(30).AddMinutes(-2);

            await _mediator.Send(attendanceBackgroundProcessJob);
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} AttendanceProcessedHostedJob is ended.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AttendanceProcessedHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
