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
using static CompanySetup.Application.MonthCycle.Commands.MonthCycleCommands;

namespace ASL.Hrms.Api.HostedServices
{
    public class ProcessMonthCycleCreateHostedJob : CronJobService
    {
        private readonly ILogger<ProcessMonthCycleCreateHostedJob> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public ProcessMonthCycleCreateHostedJob(IScheduleConfig<ProcessMonthCycleCreateHostedJob> config, ILogger<ProcessMonthCycleCreateHostedJob> logger, IMediator mediator, IConfiguration configuration)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessMonthCycleCreateHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessMonthCycleCreateHostedJob is starting.");
            try
            {
                var command = new ProcessMonthCycleCreate
                {
                    ProcessDate = DateTime.Now.Date
                };

                var data = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:hh:mm:ss} ProcessMonthCycleCreateHostedJob got exception. {ex.Message}");
            }
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ProcessMonthCycleCreateHostedJob is ended.");
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessMonthCycleCreateHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
