﻿using Microsoft.Extensions.DependencyInjection;

namespace TripManager.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}