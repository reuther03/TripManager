﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManager.Application;
using TripManager.Application.Behaviors;
using TripManager.Infrastructure.Auth;
using TripManager.Infrastructure.Database;
using TripManager.Infrastructure.Middlewares;
using TripManager.Infrastructure.Utilities.Swagger;

namespace TripManager.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ExceptionMiddleware>();

        services.AddControllers();
        services.AddDatabase(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        services.AddAuth(configuration);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies([
                typeof(IApplicationAssembly).Assembly,
                typeof(IInfrastructureAssembly).Assembly
            ]);

            config.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwaggerDocumentation();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}