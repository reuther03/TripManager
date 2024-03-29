using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;

namespace TripManager.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {Request}, {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => failure)
            .Distinct()
            .ToList();

        if (errors.Count != 0)
        {
            _logger.LogError("Invalid request {Request}, {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
            throw new ApplicationValidationException("Validation failed: {0}", errors);
        }

        var response = await next();
        _logger.LogInformation("Completed request {Request}, {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        return response;
    }
}