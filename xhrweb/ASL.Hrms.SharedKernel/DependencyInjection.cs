using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IImageFileManager, ImageFileManager>();
            services.AddScoped<IUriComposer, UriComposer>();
            services.AddScoped<IFileManager, FileManager>();


            return services;
        }
    }
}
