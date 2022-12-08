//using PayrollManagement.Core.Interfaces;
//using PayrollManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayrollManagement.Core.Interfaces;
using PayrollManagement.Persistence.Repositories;

namespace PayrollManagement.Persistence
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddPayrollManagementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PayrollContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"), x => x.MigrationsHistoryTable("__MigrationHistory", "payroll")));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));
            services.AddScoped(typeof(IEmployeeSalaryProcessedDataRepository), typeof(EmployeeSalaryProcessedDataRepository));
            services.AddScoped(typeof(IEmployeeBonusProcessedDataRepository), typeof(EmployeeBonusProcessedDataRepository));

            return services;
        }
    }
}
