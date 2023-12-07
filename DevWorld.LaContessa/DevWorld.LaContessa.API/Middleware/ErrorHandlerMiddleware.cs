using System.Net;
using System.Text.Json;

namespace DevWorld.LaContessa.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly LaContessaProblemDetailsFactory _laContessaProblemDetailsFactory;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger,
        LaContessaProblemDetailsFactory laContessaProblemDetailsFactory)
    {
        _next = next;
        _logger = logger;
        _laContessaProblemDetailsFactory = laContessaProblemDetailsFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var problemDetails = _laContessaProblemDetailsFactory.Create(context, error);
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                _logger.LogError(
                    error,
                    "Error for: {ContextRequestMethod}, with StatusCode: {StatusCode}, with ErrorType: {ErrorType}, with ErrorMessage: {ErrorMessage}",
                    context.Request.Method,
                    context.Response.StatusCode,
                    error.GetType(),
                    error.Message
                );
            else
                _logger.LogWarning(
                    error,
                    "Warning for: {ContextRequestMethod}, with StatusCode: {StatusCode}, with ErrorType: {ErrorType}, with ErrorMessage: {ErrorMessage}",
                    context.Request.Method,
                    context.Response.StatusCode,
                    error.GetType(),
                    error.Message
                );

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}