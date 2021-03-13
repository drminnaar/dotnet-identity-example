using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class SwaggerSetup
    {
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Events API - V1",
                        Version = "v1",
                        Description = "Events API is a demo API that demonstrates how to use Asp.Net Identity"
                    });
                setup.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                setup.CustomSchemaIds(ApplySchemaIdStrategy);
                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\". Enter only the value of the token below:"
                });
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.EnableAnnotations();
                setup.IncludeXmlComments(xmlPath);
            });
        }

        internal static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API V1");
                setup.RoutePrefix = string.Empty;
                setup.DefaultModelExpandDepth(2);
                setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                setup.EnableDeepLinking();
            });
            return app;
        }

        private static string ApplySchemaIdStrategy(Type type)
        {
            var returnedValue = type.Name;
            if (returnedValue.ToLower().EndsWith("dto"))
                returnedValue = returnedValue.Replace("dto", string.Empty, StringComparison.CurrentCultureIgnoreCase);
            return returnedValue;
        }
    }
}
