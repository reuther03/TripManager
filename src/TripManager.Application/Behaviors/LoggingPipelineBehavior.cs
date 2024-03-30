using MediatR;
using Microsoft.Extensions.Logging;

namespace TripManager.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private const string QuerySuffix = "Query";
    private const string CommandSuffix = "Command";

    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var baseTypeName = GetBaseTypeName(request);

        _logger.LogInformation(
            "[{Timestamp} | {RequestType}] Handling {RequestName}",
            DateTime.UtcNow,
            baseTypeName,
            typeof(TRequest).Name);

        try
        {
            var response = await next();

            _logger.LogInformation(
                "[{Timestamp} | {RequestType}] Handled successfully {RequestName}",
                DateTime.UtcNow,
                baseTypeName,
                typeof(TRequest).Name);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "[{Timestamp} | {RequestType}] Error handling {RequestName}",
                DateTime.UtcNow,
                baseTypeName,
                typeof(TRequest).Name);

            throw;
        }
    }

    private static string GetBaseTypeName(TRequest request)
    {
        var typeName = request.GetType().Name;

        if (typeName.Contains(QuerySuffix))
            return QuerySuffix;

        if (typeName.Contains(CommandSuffix))
            return CommandSuffix;

        return "Unknown";
    }
}