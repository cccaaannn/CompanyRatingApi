namespace CompanyRateApi.Shared.Auth;

public interface ITokenUtils
{
    public AccessTokenDto GenerateAccessToken(AccessTokenClaims claims);
    
    public TokenDto GenerateToken(
        Dictionary<string, string> claims,
        int expirationHours = 1
    );
}