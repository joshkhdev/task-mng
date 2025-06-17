using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Utils.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is RestException)
        {
            _logger.LogError(exception.Message);
        }
        else
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        }

        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case RestBadRequestException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Bad Request";
                problemDetails.Detail = exception.Message;
                break;
            case RestUnauthorizedException:
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                problemDetails.Title = "Unauthorized";
                problemDetails.Detail = exception.Message;
                break;
            case RestForbiddenException:
                problemDetails.Status = StatusCodes.Status403Forbidden;
                problemDetails.Title = "Forbidden";
                problemDetails.Detail = exception.Message;
                break;
            case RestNotFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Not Found";
                problemDetails.Detail = exception.Message;
                break;
            case RestConflictException:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = "Conflict";
                problemDetails.Detail = exception.Message;
                break;
            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Server Error";
                problemDetails.Detail = exception.Message;
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}