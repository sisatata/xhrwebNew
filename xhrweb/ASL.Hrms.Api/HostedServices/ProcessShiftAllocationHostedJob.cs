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
using static Attendance.Application.ShiftAllocation.Commands.ShiftAllocationCommands.V1;

namespace ASL.Hrms.Api.HostedServices
{
    public class ProcessShiftAllocationHostedJob : CronJobService
    {
        private readonly ILogger<ProcessShiftAllocationHostedJob> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public ProcessShiftAllocationHostedJob(IScheduleConfig<ProcessShiftAllocationHostedJob> config, ILogger<ProcessShiftAllocationHostedJob> logger, IMediator mediator, IConfiguration configuration)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessShiftAllocationHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessShiftAllocationHostedJob is starting.");
            try
            {
                var command = new ProcessShiftAllocationBulk
                {
                    ProcessDate = DateTime.Now.AddDays(2).Date
                };

                var data = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:hh:mm:ss} ProcessShiftAllocationHostedJob got exception. {ex.Message}");
            }
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessShiftAllocationHostedJob is ended.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessShiftAllocationHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
