using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.Activities;
using TripManager.Domain.Users;

namespace TripManager.Infrastructure.Database.Repository;

public class TripRepository : ITripRepository
{
    private readonly TripDbContext _dbContext;

    public TripRepository(TripDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Trip>> GetAllAsync(UserId id, CancellationToken cancellationToken = default)
        => await _dbContext.Trips
            .Where(x => x.UserId == id)
            .Include(x => x.Activities)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

    public async Task<Trip?> GetByIdAsync(TripId id, CancellationToken cancellationToken = default)
        => await _dbContext.Trips
            .Include(x => x.Activities)
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Trip trip, CancellationToken cancellationToken = default)
        => await _dbContext.Trips.AddAsync(trip, cancellationToken);

    public void Delete(Trip trip, CancellationToken cancellationToken = default)
        => _dbContext.Trips.Remove(trip);

    public async void DeleteActivity(TripId tripId, TripActivityId activityId, CancellationToken cancellationToken = default)
    {
        var activity = await _dbContext.Set<TripActivity>().SingleOrDefaultAsync(x => x.TripId == tripId && x.Id == activityId, cancellationToken);
        if (activity is null)
        {
            return;
        }

        _dbContext.Set<TripActivity>().Remove(activity);
    }
}