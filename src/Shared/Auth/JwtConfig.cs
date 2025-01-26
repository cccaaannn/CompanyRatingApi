namespace CompanyRateApi.Shared.Auth;

public record JwtConfig
{
    public static string FieldName { get; } = "JWT";

    public string SecretKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}