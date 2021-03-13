using Identity.Data;
using Identity.Data.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class IdentitySetup
    {
        internal static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}
