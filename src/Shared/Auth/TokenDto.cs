namespace CompanyRateApi.Shared.Auth;

public record TokenDto
{
    public required string Token { get; init; }

    public required DateTime Expires { get; init; }
}