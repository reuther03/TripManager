using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
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
        services.AddDbContext<TripDbContext>(dbContextOptionsBuilder => { dbContextOptionsBuilder.UseNpgsql(postgresOptions.ConnectionString); });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<ITripDbContext, TripDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}