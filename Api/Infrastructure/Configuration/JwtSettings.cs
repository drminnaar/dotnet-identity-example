using System;

namespace Api.Infrastructure.Configuration
{
    public sealed class JwtSettings
    {
        public static readonly string ConfigurationSectionName = nameof(JwtSettings);

        public int ExpiryTimeInSeconds { get; init; }

        internal TimeSpan ExpiryTimeSpanInSeconds => TimeSpan.FromSeconds(ExpiryTimeInSeconds);
    }
}
