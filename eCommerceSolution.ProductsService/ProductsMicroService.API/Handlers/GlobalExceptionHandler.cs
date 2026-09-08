using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Exception;

namespace ProductsMicroService.API.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, extensions) = MapException(exception);

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Ocorreu um erro inesperado.");
        }

        httpContext.Response.StatusCode = statusCode;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
                Extensions = extensions,
            }
        });
    }

    private static (int StatusCode, string Title, Dictionary<string, object?> Extensions) MapException(
        System.Exception exception)
    {
        return exception switch
        {
            ErrorOnValidationException validationException => (
                StatusCodes.Status400BadRequest,
                "One or more validation errors occurred.",
                new Dictionary<string, object?> { ["errors"] = validationException.ErrorMessages }),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                exception.Message,
                new Dictionary<string, object?>()),
            //
            // ForbiddenException => (
            //     StatusCodes.Status403Forbidden,
            //     exception.Message,
            //     new Dictionary<string, object?>()),
            //
            // NotFoundException => (
            //     StatusCodes.Status404NotFound,
            //     exception.Message,
            //     new Dictionary<string, object?>()),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                new Dictionary<string, object?>()),
        };
    }
}