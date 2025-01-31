namespace CompanyRatingApi.Application.Authentication.Handlers.Register;

public record RegisterRequest
{
    public string Email { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}