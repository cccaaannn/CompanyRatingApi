using CompanyRatingApi.Application.Authentication.Configurations;
using CompanyRatingApi.Application.Authentication.Services;
using CompanyRatingApi.Middlewares;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.Extensions.Options;

namespace CompanyRatingApi;

public static class ServiceRegistrarExtension
{
    public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.FieldName));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);

        services.Configure<AdminUserConfig>(configuration.GetSection(AdminUserConfig.FieldName));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<AdminUserConfig>>().Value);

        services.AddScoped<ITokenUtils, TokenUtils>();
        services.AddScoped<IHashUtils, HashUtils>();

        services.AddHostedService<InitialAdminUserHostedService>();
        services.AddHostedService<AutoMigrationHostedService>();

        services.RegisterCurrentUser();
    }
}