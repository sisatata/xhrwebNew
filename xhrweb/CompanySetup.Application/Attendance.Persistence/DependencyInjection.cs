using Attendance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Attendance.Persistence.Repositories;


namespace Attendance.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAttendancePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AttendanceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase"),x=>x.MigrationsHistoryTable("__MigrationHistory","attendance")));

            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HrmsDatabase")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IReferenceRepository<,>), typeof(ReferenceRepository<,>));
            services.AddScoped(typeof(IAttendanceProcessDataRepository), typeof(AttendanceProcessDataRepository));
            services.AddScoped(typeof(IShiftAllocationProcessRepository), typeof(ShiftAllocationProcessRepository));
            return services;
        }
    }
}
