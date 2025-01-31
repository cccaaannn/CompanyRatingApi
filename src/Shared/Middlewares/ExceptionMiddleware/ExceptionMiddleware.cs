using System.Text.Json;
using CompanyRatingApi.Shared.Exceptions;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace CompanyRatingApi.Shared.Middlewares.ExceptionMiddleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogInformation("Handling exception: {exception}", exception.Message);

        context.Response.ContentType = "application/json";

        var errorResponse = exception switch
        {
            NotFoundException => new ErrorResponse(exception.Message, HttpStatusCode.Status404NotFound),
            UnauthorizedAccessException => new ErrorResponse(exception.Message, HttpStatusCode.Status401Unauthorized),
            ForbiddenException => new ErrorResponse(exception.Message, HttpStatusCode.Status403Forbidden),
            ValidationException ex =>
                new ErrorResponse(
                    ex.ValidationResult.ErrorMessage ?? exception.Message,
                    HttpStatusCode.Status400BadRequest, ex.ValidationResult.MemberNames
                ),
            _ => HandleUnexpectedException(exception)
        };

        context.Response.StatusCode = errorResponse.GetStatusInt();

        var errorResponseJson = JsonSerializer.Serialize(errorResponse);

        return context.Response.WriteAsync(errorResponseJson);
    }

    private ErrorResponse HandleUnexpectedException(Exception exception)
    {
        logger.LogError("Unexpected exception thrown: {exception}", exception);
        return new ErrorResponse("Internal Server Error", HttpStatusCode.Status500InternalServerError);
    }
}