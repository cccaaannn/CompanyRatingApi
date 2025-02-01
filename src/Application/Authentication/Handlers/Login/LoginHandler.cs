using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Authentication.Handlers.Login;

public class LoginHandler(
    ApplicationDbContext dbContext,
    ITokenUtils tokenUtils,
    IHashUtils hashUtils
) : IHandler<LoginRequest, AccessTokenDto>
{
    public async Task<AccessTokenDto> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.AppUsers
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        if (!hashUtils.Verify(request.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var claims = new AccessTokenClaims
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };

        return tokenUtils.GenerateAccessToken(claims);
    }
}