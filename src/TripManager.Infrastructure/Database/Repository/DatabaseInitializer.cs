using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripManager.Domain.Users;
using TripManager.Domain.Users.ValueObjects;

namespace TripManager.Infrastructure.Database.Repository;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TripDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        // Seed data
        await SeedAdminUserAsync(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    // Seed data
    private async Task SeedAdminUserAsync(TripDbContext dbContext)
    {
        var adminUser = User.CreateAdmin("admin@admin.pl", "admin", Password.Create("adminadmin"), "Admin Admin");
        dbContext.Users.Add(adminUser);
        await dbContext.SaveChangesAsync();
    }
}