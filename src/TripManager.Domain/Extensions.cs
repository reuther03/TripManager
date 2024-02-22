using Microsoft.Extensions.DependencyInjection;

namespace TripManager.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}