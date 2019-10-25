using System;
using Microsoft.Extensions.Configuration;

namespace Identity.Database.Seeder
{
    public static class ConfigurationExtensions
    {
        public static SeederSettings GetSeederSettings(this IConfiguration configuration)
        {
            var seederSettings = configuration.GetSection("SeederSettings").Get<SeederSettings>();

            if (seederSettings == null)
                throw new Exception($"A value for configuration type '{nameof(SeederSettings)}' cannot be determined.");

            return seederSettings;
        }
    }
}