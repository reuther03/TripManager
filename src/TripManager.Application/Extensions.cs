using Microsoft.Extensions.DependencyInjection;
using TripManager.Common.Abstractions;

namespace TripManager.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}