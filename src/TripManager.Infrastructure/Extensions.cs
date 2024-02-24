﻿using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManager.Application;
using TripManager.Application.Abstractions;
using TripManager.Application.Features.Users.Commands;
using TripManager.Infrastructure.Authentication;
using TripManager.Infrastructure.Database;
using TripManager.Infrastructure.Utilities.Swagger;

namespace TripManager.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDatabase(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies([
                typeof(IApplicationAssembly).Assembly,
                typeof(IInfrastructureAssembly).Assembly
            ]);
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwaggerDocumentation();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}