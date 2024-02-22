using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.Activities;
using TripManager.Domain.Users;

namespace TripManager.Infrastructure.Database;

internal sealed class TripDbContext : DbContext, ITripDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripActivity> TripActivities => Set<TripActivity>();

    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}