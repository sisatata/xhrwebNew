using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLeaveManagementCore(this IServiceCollection services)
        {
            services.AddScoped<ILeaveSetupService, LeaveSetupService>();
            return services;
        }
    }
}
