using ASL.AccessControl.Identity;
using ASL.AccessControl.Permissions;
using ASL.AccessControl.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using System.Threading.Tasks;

namespace ASL.AccessControl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AccessControlDependency(this IServiceCollection services, string databaseConnectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(databaseConnectionString));
            //services.AddScoped<IPermissionProvider, StandardPermissionProvider>();
            services.AddDbContext<ReferenceContext>(options =>
                options.UseNpgsql(databaseConnectionString));

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<ITokenBasedAuthService, TokenBasedAuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            return services;
        }
        

        public static IServiceCollection AccessControlDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("IdentityDbConnection")));
            //services.AddScoped<IPermissionProvider, StandardPermissionProvider>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<ITokenBasedAuthService, TokenBasedAuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }

        public static IServiceCollection AccessControlJwtConfigure(this IServiceCollection services, string tokenKey, string tokenIssuer)
        {
            //configure identity service
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<ApplicationUser>>();
                if (existingUserManager == null)
                {
                    services.AddIdentity<ApplicationUser, IdentityRole>(option =>
                    {
                        option.SignIn.RequireConfirmedEmail = true;
                        option.User.RequireUniqueEmail = true;
                    }).AddDefaultTokenProviders()
                        .AddEntityFrameworkStores<AppIdentityDbContext>();
                }
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notify")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
