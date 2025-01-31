namespace CompanyRatingApi.Shared.Auth;

public class HashUtils: IHashUtils
{
    public string Hash(string input)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(input, 12);
    }

    public bool Verify(string input, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(input, hash);
    }
}