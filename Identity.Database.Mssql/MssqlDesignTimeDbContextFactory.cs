using System;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Database.Mssql
{
    public sealed class MssqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseSqlServer(
                "server=localhost;database=identity;user id=sa;password=P@ssword123!",
                options => options.MigrationsAssembly(GetType().Assembly.FullName));

            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}
