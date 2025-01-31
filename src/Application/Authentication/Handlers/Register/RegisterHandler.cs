using AutoMapper;
using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Exceptions;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Authentication.Handlers.Register;

public class RegisterHandler(
    ApplicationDbContext dbContext,
    ITokenUtils tokenUtils,
    IHashUtils hashUtils,
    IMapper mapper
) : IHandler<RegisterRequest, AccessTokenDto>
{
    public async Task<AccessTokenDto> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await dbContext.AppUsers
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new AlreadyExistsException();
        }

        request.Password = hashUtils.Hash(request.Password);

        var user = mapper.Map<AppUser>(request);

        dbContext.AppUsers.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        var claims = new AccessTokenClaims
        {
            Id = user.Id,
            Email = request.Email,
            Role = UserRole.User
        };

        return tokenUtils.GenerateAccessToken(claims);
    }
}