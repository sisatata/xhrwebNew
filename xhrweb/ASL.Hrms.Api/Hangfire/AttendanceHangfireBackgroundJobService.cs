using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;

namespace ASL.Hrms.Api.Hangfire
{
    public class AttendanceHangfireBackgroundJobService : IAttendanceHangfireBackgroundJobService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly ILogger<AttendanceHangfireBackgroundJobService> _logger;

        public AttendanceHangfireBackgroundJobService(IConfiguration configuration, IMediator mediator, ILogger<AttendanceHangfireBackgroundJobService> logger)
        {
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task AttendanceProcessDataForAllActiveCompaniesJob()
        {
            int.TryParse(_configuration["HangfireService:AttendanceProcessDataService:ProcessOnEveryHour"], out int processDataOnHour);

            int processDataOnMinute = 0;

            bool.TryParse(_configuration["HangfireService:AttendanceProcessDataService:IsEnabled"], out bool isEnabled);

            var jobName = nameof(AttendanceHangfireBackgroundJobService.AttendanceProcessDataForAllActiveCompaniesJob);

            RecurringJob.RemoveIfExists(jobName);

            try
            {
                if (isEnabled)
                {
                    _logger.LogInformation($"Registered background job service {nameof(AttendanceProcessDataForAllActiveCompaniesJob)} on UTC time: {DateTime.UtcNow}");

                    var currentTime = DateTime.Now;

                    RecurringJob.AddOrUpdate(jobName,
                        () => ProcessLastNHoursAttendanceOnBackgroundProcess(processDataOnHour, currentTime), $"{processDataOnMinute} */{processDataOnHour} * * *", TimeZoneInfo.Local);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to register background job service {nameof(AttendanceProcessDataForAllActiveCompaniesJob)} on UTC time: {DateTime.UtcNow}");
            }

            await Task.CompletedTask;
        }

        // Will process last one hour attendance data
        public async Task ProcessLastNHoursAttendanceOnBackgroundProcess(int processDataOnHour, DateTime scheduledTime)
        {
            var attendanceBackgroundProcessJob = new AttendanceProcessDataHangfireBackgroundCommand();
            attendanceBackgroundProcessJob.ProcessingStartDate = DateTime.Now.Date;
            attendanceBackgroundProcessJob.ProcessingEndDate = DateTime.Now.Date.AddHours(30).AddMinutes(-2);

            await _mediator.Send(attendanceBackgroundProcessJob);
        }
    }
}
