using Microsoft.OpenApi.Models;

namespace CompanyRateApi.Shared.Swagger;

public static class SwaggerServiceExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "CompanyRateApi",
                Version = "v1"
            });

            options.AddSecurityDefinition("Jwt bearer auth", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Enter bearer token without 'Bearer' into field"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Jwt bearer auth"
                        }
                    },
                    []
                }
            });
        });
    }
}