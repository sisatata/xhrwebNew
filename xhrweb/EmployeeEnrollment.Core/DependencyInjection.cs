using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLeaveManagementCore(this IServiceCollection services)
        {
            //services.AddScoped<IEmployeeDeletePostService, EmployeeDeletePostService>();
           
            return services;
        }
    }
}
