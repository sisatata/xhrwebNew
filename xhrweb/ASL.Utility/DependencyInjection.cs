using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Services;
using ASL.Utility.FileManager.Interfaces;
using ASL.Utility.FileManager.Services;
using ASL.Utility.SMSManager.Interfaces;
using ASL.Utility.SMSManager.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ASL.Utility
{
    public static class DependencyInjection
    {
        public static IServiceCollection UtilityyDependencyResolver(this IServiceCollection services)
        {
            services.AddScoped<IImageFileManager, ImageFileManager>();
            services.AddScoped<IEmailSender, EmailSender>();
            //services.AddScoped<ISMSSender, SSLCommerzSMSSender>();
            services.AddScoped<IExcelFileManager, ExcelFileManager>();
            services.AddScoped<IAttachedFileManager, AttachedFileManager>();
            return services;
        }
    }
}
