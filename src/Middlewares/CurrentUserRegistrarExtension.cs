using CompanyRatingApi.Shared.Context;

namespace CompanyRatingApi.Middlewares;

public static class CurrentUserRegistrarExtension
{
    public static void RegisterCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<CurrentUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext?.Items[nameof(CurrentUser)] as CurrentUser ?? new CurrentUser();
        });
    }
}