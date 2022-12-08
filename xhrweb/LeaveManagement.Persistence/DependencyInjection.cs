using LeaveManagement.Core.Interfaces;
using LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLeaveManagementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LeaveContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"), x => x.MigrationsHistoryTable("__MigrationHistory", "leave")));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));
            services.AddScoped(typeof(ILeaveBalanceRepository), typeof(LeaveBalanceRepository));

            return services;
        }
    }
}
