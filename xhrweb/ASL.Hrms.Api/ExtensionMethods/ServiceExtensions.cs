
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.ExtensionMethods
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .WithOrigins(configuration.GetSection("ClientUrl").Value, "http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true)
                            //.AllowCredentials()
                            );
            });
        }

    }
}
