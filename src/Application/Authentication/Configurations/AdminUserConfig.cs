namespace CompanyRatingApi.Application.Authentication.Configurations;

public record AdminUserConfig
{
    public static string FieldName { get; } = "Admin";

    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}