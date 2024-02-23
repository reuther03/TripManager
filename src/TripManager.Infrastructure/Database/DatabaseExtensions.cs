using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Common;
using TripManager.Infrastructure.Database.Repository;

// using TripManager.Infrastructure.Database.Repository;

namespace TripManager.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetRequiredSection(DatabaseSettings.SectionName));
        var postgresOptions = configuration.GetOptions<DatabaseSettings>(DatabaseSettings.SectionName);
        services.AddDbContext<TripDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<ITripDbContext, TripDbContext>();

        return services;
    }
}