using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskManagement.Core.Interfaces;
using TaskManagement.Persistence.Repositories;

namespace TaskManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTaskManagementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskMngtContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"), x => x.MigrationsHistoryTable("__MigrationHistory", "Task")));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));


            return services;
        }
    }
}
