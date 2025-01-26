namespace CompanyRateApi.Shared.Auth;

public enum UserRole
{
    User,
    Admin
}

public static class UserRoleExtension
{
    public static string GetName(this UserRole role)
    {
        return role switch
        {
            UserRole.User => nameof(UserRole.User),
            UserRole.Admin => nameof(UserRole.Admin),
            _ => "Unknown"
        };
    }

    public static UserRole FromString(string role)
    {
        return role switch
        {
            nameof(UserRole.User) => UserRole.User,
            nameof(UserRole.Admin) => UserRole.Admin,
            _ => throw new ArgumentException($"Unknown role: {role}")
        };
    }
}