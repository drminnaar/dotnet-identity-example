using Microsoft.Extensions.Configuration;

namespace Api.Configuration
{
    public sealed class JwtSecuritySettings
    {
        public static readonly string ConfigurationSectionName = nameof(JwtSecuritySettings);

        public string Secret { get; init; } = string.Empty;

        internal static JwtSecuritySettings FromConfiguration(IConfiguration configuration)
        {
            return new JwtSecuritySettings
            {
                Secret = configuration[$"{ConfigurationSectionName}:Secret"]
            };
        }
    }
}
