using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Database.Seeder
{
    internal sealed class Program
    {
        internal async static Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            var connectionStringName = args.Length == 1 ? args[0] : string.Empty;

            if (string.IsNullOrWhiteSpace(connectionStringName))
                throw new ArgumentException("Please specify connection string name as first argument.");

            var serviceProvider = new ServicesFactory(configuration)
                .CreateServices(connectionStringName)
                .BuildServiceProvider();

            var seedManager = serviceProvider.GetRequiredService<SeederManager>();

            await seedManager.SeedAsync();
        }
    }
}
