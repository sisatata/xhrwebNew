using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.HostedServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AttendanceMissPunchEmailSendHostedJob>();

            services.AddScoped<AttendanceProcessedHostedJob>();
            services.AddScoped<DatabaseBackupHostedJob>();
            services.AddScoped<ProcessPushNotificationHostedJob>();
            services.AddScoped<SendPushNotificationHostedJob>();
            services.AddScoped<ProcessShiftAllocationHostedJob>();
            services.AddScoped<ProcessMonthCycleCreateHostedJob>();
            //string sConnectionString = configuration["ConnectionStrings:HangfireConnection"];


            bool.TryParse(configuration["HostedJobService:AttendanceMissedPunchEmailService:IsEnabled"], out bool isMissPunchEnabled);
            if (isMissPunchEnabled)
            {
                var strProcessTime = configuration["HostedJobService:AttendanceMissedPunchEmailService:ProcessOnEveryDay"];
                var dtProcessTime = DateTime.Now.Date.Add(TimeSpan.Parse(strProcessTime));

                services.AddCronJob<AttendanceMissPunchEmailSendHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"{dtProcessTime.Minute} {dtProcessTime.Hour} * * *";
                });
            }
            bool.TryParse(configuration["HostedJobService:AttendanceProcessDataService:IsEnabled"], out bool isAttnProcEnabled);
            if (isAttnProcEnabled)
            {
                var strProcessTime = configuration["HostedJobService:AttendanceProcessDataService:ProcessOnEveryMinute"];
                services.AddCronJob<AttendanceProcessedHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"*/{strProcessTime} * * * *";
                });
            }

            bool.TryParse(configuration["HostedJobService:DatabaseBackupService:IsEnabled"], out bool isDatabaseBackupEnabled);
            if (isDatabaseBackupEnabled)
            {
                var strProcessTime = configuration["HostedJobService:DatabaseBackupService:ProcessOnEveryMinute"];
                services.AddCronJob<DatabaseBackupHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"*/{strProcessTime} * * * *";
                });
            }

            // ProcessPushNotificationHostedJob
            bool.TryParse(configuration["HostedJobService:ProcessPushNotificationService:IsEnabled"], out bool isProcessPushNotifiEnabled);
            if (isProcessPushNotifiEnabled)
            {
                var strProcessTime = configuration["HostedJobService:ProcessPushNotificationService:ProcessOnEveryDay"];
                var dtProcessTime = DateTime.Now.Date.Add(TimeSpan.Parse(strProcessTime));

                services.AddCronJob<ProcessPushNotificationHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"{dtProcessTime.Minute} {dtProcessTime.Hour} * * *";
                });
            }

            //
            bool.TryParse(configuration["HostedJobService:SendPushNotificationService:IsEnabled"], out bool isSendPushNotificationEnabled);
            if (isSendPushNotificationEnabled)
            {
                var strProcessTime = configuration["HostedJobService:SendPushNotificationService:ProcessOnEveryMinute"];
                services.AddCronJob<SendPushNotificationHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"*/{strProcessTime} * * * *";
                });
            }

            bool.TryParse(configuration["HostedJobService:ShiftAllocationService:IsEnabled"], out bool isShiftAllocationEnabled);
            if (isShiftAllocationEnabled)
            {
                var strProcessTime = configuration["HostedJobService:ShiftAllocationService:ProcessOnEveryDay"];
                var dtProcessTime = DateTime.Now.Date.Add(TimeSpan.Parse(strProcessTime));
                services.AddCronJob<ProcessShiftAllocationHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"{dtProcessTime.Minute} {dtProcessTime.Hour} * * *";
                });
            }

            bool.TryParse(configuration["HostedJobService:MonthCycleCreateService:IsEnabled"], out bool isMonthCycleCreateEnabled);
            if (isMonthCycleCreateEnabled)
            {
                var strProcessTime = configuration["HostedJobService:MonthCycleCreateService:ProcessOnEveryDay"];
                var dtProcessTime = DateTime.Now.Date.Add(TimeSpan.Parse(strProcessTime));
                services.AddCronJob<ProcessMonthCycleCreateHostedJob>(c =>
                {
                    c.TimeZoneInfo = TimeZoneInfo.Local;
                    //c.CronExpression = @"*/1 * * * *";
                    c.CronExpression = $"{dtProcessTime.Minute} {dtProcessTime.Hour} * * *";
                });
            }
            return services;
        }
    }
}
