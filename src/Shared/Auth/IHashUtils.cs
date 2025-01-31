namespace CompanyRatingApi.Shared.Auth;

public interface IHashUtils
{
    string Hash(string input);
    bool Verify(string input, string hash);
}