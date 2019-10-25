using System;
using System.Threading.Tasks;

namespace Identity.Database.Seeder
{
    public sealed class SeederManager
    {
        private readonly AppUserDataSeeder _appUserDataSeeder;

        public SeederManager(AppUserDataSeeder appUserDataSeeder)
        {
            _appUserDataSeeder = appUserDataSeeder 
                ?? throw new ArgumentNullException(nameof(appUserDataSeeder));
        }

        public async Task SeedAsync()
        {
            await _appUserDataSeeder.SeedAsync();
        }
    }
}