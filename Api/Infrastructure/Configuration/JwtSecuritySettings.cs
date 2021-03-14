namespace Api.Infrastructure.Configuration
{
    public sealed class JwtSecuritySettings
    {
        internal const string ConfigurationSectionName = nameof(JwtSecuritySettings);

        public string Secret { get; init; } = string.Empty;
    }
}
