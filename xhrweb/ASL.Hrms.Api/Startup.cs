using Asl.Infrastructure;
using ASL.AccessControl;
using ASL.AccessControl.Services.Security;
using ASL.Hrms.Api.ExtensionMethods;
using ASL.Hrms.Api.Filters;
using ASL.Hrms.Api.Hangfire;
using ASL.Hrms.Api.Hubs;
using ASL.Hrms.Api.PermissionProvider;
using ASL.Hrms.Api.Reports;
using ASL.Hrms.Api.Services;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility;
using ASL.Utility.EmailManager.Models;
using Attendance.Application;
using Attendance.Core.Handlers.ApplicationEventHandler;
using Attendance.Persistence;
using Autofac;
using CompanySetup.Application;
using CompanySetup.Persistence;
using EmployeeEnrollment.Application;
using EmployeeEnrollment.Persistence;
using Hangfire;
using Hangfire.PostgreSql;
using LeaveManagement.Application;
using LeaveManagement.Core;
using LeaveManagement.Core.Events;
using LeaveManagement.Core.Handlers.ApplicationEventHandler;
using LeaveManagement.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using PayrollManagement.Application;
using PayrollManagement.Core.Handlers.ApplicationEventHandler;
using PayrollManagement.Persistence;
using ASL.Hrms.Api.HostedServices;
using System;
using CompanySetup.Core.Handlers;
using EmployeeEnrollment.Core.EventHandler;
using ASL.Hrms.Api.Utility;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using Wkhtmltopdf.NetCore;
using TaskManagement.Persistence;
using TaskManagement.Application;
using PayrollManagement.Core.ApplicationEvents;
using Attendance.Core.Handlers;

namespace ASL.Hrms.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(Configuration);
            // services.AddCors();

            // for the time being resolved here
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IAttendanceProcessService, AttendanceProcessService>();
            services.AddScoped<ICompanyApprovePostService, CompanyApprovePostService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IEmployeeNoteService, EmployeeNoteService>();
            services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();
            services.AddScoped<IPushNotificationProcessService, PushNotificationProcessService>();
            services.AddScoped<ILeaveEncashmentService, LeaveEncashmentService>();

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddMessageBroker(new[]
            {
                typeof(LeaveAppliedEvent).Assembly,
                typeof(LeaveAppliedEventHandler).Assembly,
                typeof(LeaveNotificationEventHandler).Assembly,

                typeof(AttendanceAppliedEventHandler).Assembly,
                typeof(AttendanceApprovedEventHandler).Assembly,
                typeof(AttendanceDeclinedEventHandler).Assembly,
                typeof(HolidayPushNotificationProcessEventHandler).Assembly,

                typeof(BillAppliedEventHandler).Assembly,
                typeof(BillApprovedEventHandler).Assembly,
                typeof(BillDeclinedEventHandler).Assembly,
                typeof(ApproveCompanyEventHandler).Assembly,
                typeof(EmployeeDeleteEventHandler).Assembly,
                typeof(PayrollActivityLogEvent).Assembly,
                typeof(PayrollActivityLogEventHandler).Assembly
            });

            services.AddCompanySetupPersistence(Configuration);
            services.AddCompanySetupApplication(Configuration);

            services.AddEmployeeEnrollmentPersistence(Configuration);
            services.AddEmployeeEnrollmentApplication(Configuration);

            services.AddLeaveManagementPersistence(Configuration);
            services.AddLeaveManagementApplication(Configuration);
            services.AddLeaveManagementCore();

            services.AddAttendancePersistence(Configuration);
            services.AddAttendanceApplication(Configuration);

            services.AddPayrollManagementPersistence(Configuration);
            services.AddPayrollManagementApplication(Configuration);

            services.AddTaskManagementPersistence(Configuration);
            services.AddTaskManagementApplication(Configuration);

            services.AddSharedKernelPersistence(Configuration);
            services.AddHostedServices(Configuration);
            


            services.Configure<PlanetHRSettings>(Configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<PlanetHRSettings>()));
            services.UtilityyDependencyResolver();


            //printing report
            var reportingAssembly = new ReportingAssemblyLoadContext();
            reportingAssembly.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory() + "/ExternalDlls", "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.TryAddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AccessControlJwtConfigure(Configuration.GetValue<string>("Jwt:Key"), Configuration.GetValue<string>("Jwt:Issuer"));
            //services.AccessControlDependency(Configuration);
            services.AccessControlDependency(Configuration.GetConnectionString("HRMSDatabase"));
            services.AddScoped<IPermissionProvider, StandardPermissionProvider>();
            services.Configure<MailServerSettings>(Configuration.GetSection("MailServerSettings"));
            services.AddScoped<ICurrentUserContext, CurrentUserContext>();
            //services.AddTransient<IAttendanceHangfireBackgroundJobService, AttendanceHangfireBackgroundJobService>();
            //services.AddTransient<IAttendanceMissPunchEmailSendJobService, AttendanceMissPunchEmailSendJobService>();

            services.AddScoped<IExcelReportService, ExcelReportService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            //services.AddCronJob<MyCronJob1>(c =>
            //{

            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"*/2 * * * *";
            //    //c.CronExpression = $"20 10 * * *";
            //});

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aplectrum HRMS API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
            });



            //string sConnectionString = Configuration["ConnectionStrings:HangfireConnection"];
            //services.AddHangfire(x => x.UsePostgreSqlStorage(sConnectionString));

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultSharedModule(_env.EnvironmentName == "Development", typeof(LeaveAppliedEvent).Assembly));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetValue<string>("ContentRoot:RootPath")),
                RequestPath = ""
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseHttpsRedirection();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aplectrum HRMS API V1");
            });
            /*
                        app.UseCors(builder => builder
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           //.AllowCredentials()
                           );*/

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            //});

            //var options = new BackgroundJobServerOptions
            //{
            //    WorkerCount = 1    //Hangfire's default worker count is 20, which opens 20 connections simultaneously.
            //                       // For this we are overriding the default value.
            //};

            //app.UseHangfireServer(options);

            //BackgroundJob.Enqueue<IAttendanceHangfireBackgroundJobService>(x => x.AttendanceProcessDataForAllActiveCompaniesJob());
            //BackgroundJob.Enqueue<IAttendanceMissPunchEmailSendJobService>(x => x.AttendanceMissPunchEmailSendJob());

        }
    }
}
