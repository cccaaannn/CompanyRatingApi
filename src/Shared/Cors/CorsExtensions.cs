namespace CompanyRatingApi.Shared.Cors;

public static class CorsExtensions
{
    private const string CorsPolicyName = "AllowAll";

    public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
    }
}