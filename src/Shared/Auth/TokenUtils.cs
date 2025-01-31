using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CompanyRatingApi.Shared.Auth;

public class TokenUtils(JwtConfig jwtConfig) : ITokenUtils
{
    public TokenDto GenerateToken(
        Dictionary<string, string> claims,
        int expirationHours = 1
    )
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey!);
        var expires = DateTime.UtcNow.AddHours(expirationHours);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                claims.Select(claim => new Claim(claim.Key, claim.Value))
            ),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenDto { Token = tokenString, Expires = expires };
    }
    
    public AccessTokenDto GenerateAccessToken(AccessTokenClaims claims)
    {
        var claimsDict = new Dictionary<string, string>
        {
            { ClaimTypes.NameIdentifier, claims.Id.ToString() },
            { ClaimTypes.Email, claims.Email },
            { ClaimTypes.Role, claims.Role.GetName() }
        };

        var accessToken = GenerateToken(claimsDict, 1);

        return new AccessTokenDto { AccessToken = accessToken.Token, Expires = accessToken.Expires};
    }
}