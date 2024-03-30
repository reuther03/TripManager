using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TripManager.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(IApplicationAssembly).Assembly, includeInternalTypes: true);

        return services;
    }
}