using Api.Infrastructure.Configuration;
using Api.Infrastructure.Security;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class AppSetup
    {
        internal static IServiceCollection ConfigureAppServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.ConfigurationSectionName));
            services.Configure<JwtSecuritySettings>(configuration.GetSection(JwtSecuritySettings.ConfigurationSectionName));
            services.AddScoped<TokenService>();
            return services;
        }
    }
}
