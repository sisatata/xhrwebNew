using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data.Common;
using System.Reflection;
using PayrollManagement.Core.Interfaces;

namespace PayrollManagement.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddPayrollManagementApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<DbConnection>(c => new NpgsqlConnection(configuration.GetConnectionString("HrmsDatabase")));
            
            return services;
        }
    }
}
