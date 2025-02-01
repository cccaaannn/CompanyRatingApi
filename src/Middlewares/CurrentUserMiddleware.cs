using System.Security.Claims;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Context;

namespace CompanyRatingApi.Middlewares;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
        {
            var currentUser = new CurrentUser()
            {
                Id = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty),
                Email = identity.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                Role = UserRoleExtension.FromString(identity.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty)
            };

            context.Items[nameof(CurrentUser)] = currentUser;
        }

        await _next(context);
    }
}
