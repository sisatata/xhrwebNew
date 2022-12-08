﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;
using System.Reflection;

namespace LeaveManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLeaveManagementApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<DbConnection>(c => new NpgsqlConnection(configuration.GetConnectionString("HrmsDatabase")));

            return services;
        }
    }
}
