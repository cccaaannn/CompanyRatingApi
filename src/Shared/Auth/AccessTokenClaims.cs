namespace CompanyRateApi.Shared.Auth;

public record AccessTokenClaims
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required UserRole Role { get; init; }
}