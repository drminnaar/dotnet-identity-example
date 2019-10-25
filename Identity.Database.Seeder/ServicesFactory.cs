using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Identity.Data;
using Identity.Data.Models;

namespace Identity.Database.Seeder
{
    public sealed class ServicesFactory
    {
        private readonly IConfiguration _configuration;

        public ServicesFactory(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IServiceCollection CreateServices(string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentException(
                    $"The '{nameof(connectionStringName)}' may not be null, empty, or whitespace.",
                    nameof(connectionStringName));
            }

            var services = new ServiceCollection();

            services.AddLogging(configure => configure.AddConsole());

            var seederSettings = _configuration.GetSeederSettings();

            services.AddSingleton<SeederSettings>(seederSettings);

            services.AddDbContextPool<AppIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();

                connectionStringName = connectionStringName.Trim().ToLower();
                const string SqliteConnectionString = "identity_sqlite";
                const string PostgresConnectionString = "identity_postgres";
                const string MssqlConnectionString = "identity_mssql";

                if (connectionStringName == SqliteConnectionString)
                {
                    optionsBuilder.UseSqlite(_configuration.GetConnectionString(SqliteConnectionString));
                }
                else if (connectionStringName == PostgresConnectionString)
                {
                    optionsBuilder.UseNpgsql(_configuration.GetConnectionString(PostgresConnectionString));
                }
                else if (connectionStringName == MssqlConnectionString)
                {
                    optionsBuilder.UseSqlServer(_configuration.GetConnectionString(MssqlConnectionString));
                }
                else
                {
                    throw new NotSupportedException($"The specified connectionString name '{connectionStringName}' is not supported");
                }
            });

            services
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddSingleton<JsonFileParser>();

            services.AddSingleton<AppUserDataSeeder>(provider =>
            {
                var dbContext = provider.GetRequiredService<AppIdentityDbContext>();
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var jsonFileParser = provider.GetRequiredService<JsonFileParser>();

                return new AppUserDataSeeder(
                    dbContext,
                    userManager,
                    jsonFileParser,
                    seederSettings.DefaultUserPassword,
                    seederSettings.AppUserDataJsonFilePath);
            });

            services.AddTransient<SeederManager>();

            return services;
        }
    }
}
