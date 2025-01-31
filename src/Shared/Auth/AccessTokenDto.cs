namespace CompanyRatingApi.Shared.Auth;

public record AccessTokenDto
{
    public required string AccessToken { get; init; }

    public required DateTime Expires { get; init; }
}