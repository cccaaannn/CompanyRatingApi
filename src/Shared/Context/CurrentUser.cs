using CompanyRatingApi.Shared.Auth;

namespace CompanyRatingApi.Shared.Context;

public record CurrentUser
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Email { get; init; } = string.Empty;
    public UserRole Role { get; init; }
}