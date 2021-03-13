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
            services.AddDbContextPool<AppIdentityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("identity"));
                options.EnableDetailedErrors(environment.IsDevelopment());
                options.EnableSensitiveDataLogging(environment.IsDevelopment());
            });
            return services;
        }
    }
}
