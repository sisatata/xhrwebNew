using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ASL.AccessControl.Identity
{
    public class AppIdentityContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();

            var connectionString = configuration
                        .GetConnectionString("HRMSDatabase");

            dbContextBuilder.UseNpgsql(connectionString);

            return new AppIdentityDbContext(dbContextBuilder.Options);
        }
    }
}
