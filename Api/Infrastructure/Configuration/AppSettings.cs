namespace Api.Infrastructure.Configuration
{
    public sealed class AppSettings
    {
        internal const string ConfigurationSectionName = nameof(AppSettings);

        public string ConnectionStringName { get; set; } = string.Empty;
    }
}
