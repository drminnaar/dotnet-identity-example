using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ControllerSetup
    {
        internal static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState));
                    };
                    options.ClientErrorMapping[StatusCodes.Status400BadRequest].Link = "https://httpstatuses.com/400";
                    options.ClientErrorMapping[StatusCodes.Status401Unauthorized].Link = "https://httpstatuses.com/401";
                    options.ClientErrorMapping[StatusCodes.Status403Forbidden].Link = "https://httpstatuses.com/403";
                    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
                    options.ClientErrorMapping[StatusCodes.Status422UnprocessableEntity].Link = "https://httpstatuses.com/422";
                    options.ClientErrorMapping[StatusCodes.Status500InternalServerError].Link = "https://httpstatuses.com/500";
                });

            return services;
        }
    }
}
