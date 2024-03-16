using MediatR;
using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database;
using TripManager.Common.Primitives.Domain;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.Activities;
using TripManager.Domain.Users;

namespace TripManager.Infrastructure.Database;

public sealed class TripDbContext : DbContext, ITripDbContext
{
    private readonly IPublisher _publisher;

    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripActivity> TripActivities => Set<TripActivity>();

    public TripDbContext(DbContextOptions<TripDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        PublishDomainEvents();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void PublishDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();

        foreach (var entity in domainEntities)
        {
            var events = entity.Entity.DomainEvents.ToList();
            entity.Entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                _publisher.Publish(domainEvent);
            }
        }
    }
}