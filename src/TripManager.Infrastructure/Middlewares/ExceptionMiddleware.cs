using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TripManager.Common.Exceptions.Application;
using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives.Envelopes;

namespace TripManager.Infrastructure.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError("An error occurred: {Message}", exception.Message);
            var statusCode = exception switch
            {
                ApplicationValidationException => StatusCodes.Status400BadRequest,
                DomainException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new Envelope
            {
                StatusCode = statusCode,
                Error = exception.Message
            });
        }
    }
}