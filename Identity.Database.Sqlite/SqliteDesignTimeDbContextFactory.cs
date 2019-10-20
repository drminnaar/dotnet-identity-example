using System;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Database.Sqlite
{
    public sealed class SqliteDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(
                "DataSource=/home/dmin/data/identity/identity.db",
                options => options.MigrationsAssembly(this.GetType().Assembly.FullName));
            
            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}
