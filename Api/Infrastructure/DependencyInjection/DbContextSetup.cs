using System;
using Api.Infrastructure.Configuration;
using Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class DbContextSetup
    {
        internal static IServiceCollection ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            var appSettings = configuration.GetAppSettings();

            if (appSettings.ConnectionStringName.ToLower() == "identity_postgres")
            {
                services.AddDbContextPool<AppIdentityDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(appSettings.ConnectionStringName));
                    options.EnableDetailedErrors(environment.IsDevelopment());
                    options.EnableSensitiveDataLogging(environment.IsDevelopment());
                });
            }
            else if (appSettings.ConnectionStringName.ToLower() == "identity_mssql")
            {
                services.AddDbContextPool<AppIdentityDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(appSettings.ConnectionStringName));
                    options.EnableDetailedErrors(environment.IsDevelopment());
                    options.EnableSensitiveDataLogging(environment.IsDevelopment());
                });
            }
            else if (appSettings.ConnectionStringName.ToLower() == "identity_sqlite")
            {
                services.AddDbContextPool<AppIdentityDbContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString(appSettings.ConnectionStringName));
                    options.EnableDetailedErrors(environment.IsDevelopment());
                    options.EnableSensitiveDataLogging(environment.IsDevelopment());
                });
            }
            else
            {
                throw new NotSupportedException(
                    $"The specified connectionString name '{appSettings.ConnectionStringName}' is not supported");
            }
            return services;
        }
    }
}
