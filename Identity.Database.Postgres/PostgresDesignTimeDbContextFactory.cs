using System;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Database.Postgres
{
    public sealed class PostgresDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseNpgsql(
                "server=localhost;database=identity;port=5432;user id=postgres;password=password",
                options => options.MigrationsAssembly(GetType().Assembly.FullName));

            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}
