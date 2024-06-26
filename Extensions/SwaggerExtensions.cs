using Microsoft.OpenApi.Models;

namespace BankingControlPanel.Extensions
{
    internal static class SwaggerExtensions
    {
        #region Public Static Functions
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Define the Swagger document with title and version
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BankingControlPanel API", Version = "v1" });

                // Add JWT Bearer authentication definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Add security requirements to use the defined Bearer scheme
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
            return services;
        }
        #endregion
    }
}
