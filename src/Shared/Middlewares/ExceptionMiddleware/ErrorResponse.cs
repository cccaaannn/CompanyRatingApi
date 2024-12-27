using System.Text.Json.Serialization;

namespace CompanyRateApi.Shared.Middlewares.ExceptionMiddleware;

public class ErrorResponse
{
    public string Title { get; init; }
    
    public HttpStatusCode Status { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string> Errors { get; init; }
    
    public ErrorResponse(string title)
    {
        Title = title;
        Status = HttpStatusCode.Status500InternalServerError;
        Errors = null!;
    }
    
    public ErrorResponse(string title, HttpStatusCode statusCode)
    {
        Title = title;
        Status = statusCode;
        Errors = null!;
    }
    
    public ErrorResponse(string title, HttpStatusCode statusCode, IEnumerable<string> errors)
    {
        Title = title;
        Status = statusCode;
        Errors = errors;
    }

    public int GetStatusInt()
    {
        return (int)Status;
    }
}