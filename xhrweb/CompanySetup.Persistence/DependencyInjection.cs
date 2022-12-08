using CompanySetup.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanySetup.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCompanySetupPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompanyContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"), x => x.MigrationsHistoryTable("__MigrationHistory", "main")
                                                                                            .EnableRetryOnFailure()));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));
            services.AddScoped(typeof(INotificationBulkRepository), typeof(NotificationBulkRepository));
            services.AddScoped(typeof(IMonthCycleProcessRepository), typeof(MonthCycleProcessRepository));
            return services;
        }
    }
}
