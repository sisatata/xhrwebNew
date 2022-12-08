using System.Reflection;
using System.Threading.Tasks;
using ASL.AccessControl.Identity;
using ASL.AccessControl.Services.Security;
using Attendance.Persistence;
using Autofac.Extensions.DependencyInjection;
using CompanySetup.Persistence;
using EmployeeEnrollment.Persistence;
using LeaveManagement.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PayrollManagement.Persistence;
using Serilog;
using TaskManagement.Persistence;

namespace ASL.Hrms.Api
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        CreateHostBuilder(args).Build().Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            });
    //}

    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            // apply database migrations here
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var hostingEnvironment = services.GetService<IWebHostEnvironment>();
                // if needed we can check hosting environment
                //if(hostingEnvironment.IsDevelopment())

                // migrate company context
                var companyContext = services.GetRequiredService<CompanyContext>();
               companyContext.Database.Migrate();
                // migrate leave context
                var leaveContext = services.GetRequiredService<LeaveContext>();
               leaveContext.Database.Migrate();
               
                // migrate leave context
                var employeeContext = services.GetRequiredService<EmployeeContext>();
                employeeContext.Database.Migrate();

                // migration attendance context
               var attendanceContext = services.GetRequiredService<AttendanceContext>();
                attendanceContext.Database.Migrate();

                // migration payroll context
                var payrollContext = services.GetRequiredService<PayrollContext>();
                payrollContext.Database.Migrate();

                // migration attendance context
              var appIdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
               appIdentityDbContext.Database.Migrate();

                // migration Task context
                var taskDbContext = services.GetRequiredService<TaskMngtContext>();
                taskDbContext.Database.Migrate();

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var permissionProvider = services.GetRequiredService<IPermissionProvider>();
                var seedContext = new AppIdentityDbContextSeed(userManager, roleManager, permissionProvider);
                
                await seedContext.SeedDefaultUserAsync();
                await seedContext.SeedAdminUserAsync("aplectrumsolutions@gmail.com", "Aplectrum1234$");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, config) => { config.ReadFrom.Configuration(ctx.Configuration); })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging(logging =>
                        {
                            //logging.ClearProviders();
                            //logging.AddConsole();
                            // logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure
                        });
                });

        //    public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        //        Host.CreateDefaultBuilder(args)
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //            .ConfigureAppConfiguration((hostingContext, config) =>
        //            {
        //                var env = hostingContext.HostingEnvironment;

        //                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        //                    .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

        //                if (env.IsDevelopment())
        //                {
        //                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
        //                    if (appAssembly != null)
        //                    {
        //                        config.AddUserSecrets(appAssembly, optional: true);
        //                    }
        //                }

        //                config.AddEnvironmentVariables();

        //                if (args != null)
        //                {
        //                    config.AddCommandLine(args);
        //                }
        //            })
        //            .UseStartup<Startup>();
    }

}
