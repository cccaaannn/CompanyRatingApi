namespace CompanyRatingApi.Shared.Middlewares.ExceptionMiddleware;

public enum HttpStatusCode
{
    Status400BadRequest = 400,
    Status401Unauthorized = 401,
    Status403Forbidden = 403,
    Status404NotFound = 404,
    Status500InternalServerError = 500
}
