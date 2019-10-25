using System.IO;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Identity.Database.Postgres
{
    public sealed class PostgresDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();
                
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseNpgsql(
                configuration.GetConnectionString("default"),
                options => options.MigrationsAssembly(GetType().Assembly.FullName));

            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}
