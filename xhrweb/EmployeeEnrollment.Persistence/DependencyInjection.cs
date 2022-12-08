using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeEnrollment.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmployeeEnrollmentPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"), x => x.MigrationsHistoryTable("__MigrationHistory", "employee")));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));
            services.AddScoped(typeof(IEmployeeDevicesRepository), typeof(EmployeeDevicesRepository));
            services.AddScoped(typeof(IEmployeeRawDataPrepRepository), typeof(EmployeeRawDataPrepRepository));
            return services;
        }
    }
}
