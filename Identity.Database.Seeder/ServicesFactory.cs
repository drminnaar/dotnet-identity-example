using System;
using Identity.Data;
using Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddLogging(options => options.AddConsole());
            services.AddDbContextPool<AppIdentityDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();

                connectionStringName = connectionStringName.Trim().ToLower();
                const string SqliteConnectionString = "identity_sqlite";
                const string PostgresConnectionString = "identity_postgres";
                const string MssqlConnectionString = "identity_mssql";

                switch (connectionStringName)
                {
                    case (SqliteConnectionString):
                        options.UseSqlite(_configuration.GetConnectionString(SqliteConnectionString));
                        break;
                    case (PostgresConnectionString):
                        options.UseNpgsql(_configuration.GetConnectionString(PostgresConnectionString));
                        break;
                    case (MssqlConnectionString):
                        options.UseSqlServer(_configuration.GetConnectionString(MssqlConnectionString));
                        break;
                    default:
                        throw new NotSupportedException($"The specified connectionString name '{connectionStringName}' is not supported");
                }
            });
            services
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddSingleton(_configuration.GetSeederSettings());
            services.AddSingleton<JsonFileParser>();
            services.AddSingleton(provider =>
            {
                var dbContext = provider.GetRequiredService<AppIdentityDbContext>();
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                var jsonFileParser = provider.GetRequiredService<JsonFileParser>();
                var seederSettings = provider.GetRequiredService<SeederSettings>();

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
