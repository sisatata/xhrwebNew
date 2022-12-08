using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;
using System.Reflection;

namespace EmployeeEnrollment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmployeeEnrollmentApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<DbConnection>(c => new NpgsqlConnection(configuration.GetConnectionString("HrmsDatabase")));

            return services;
        }
    }
}
