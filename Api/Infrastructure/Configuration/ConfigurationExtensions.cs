using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static JwtSecuritySettings GetJwtSecuritySettings(this IConfiguration configuration)
        {
            var settings = new JwtSecuritySettings();
            configuration.GetSection(JwtSecuritySettings.ConfigurationSectionName).Bind(settings);
            return settings;
        }

        public static AppSettings GetAppSettings(this IConfiguration configuration)
        {
            var settings = new AppSettings();
            configuration.GetSection(AppSettings.ConfigurationSectionName).Bind(settings);
            return settings;
        }
    }
}
