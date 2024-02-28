using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Domain.Trips;
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
            .AsNoTracking()
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task UpdateAsync(Trip trip, CancellationToken cancellationToken = default)
    {
        _dbContext.Trips.Update(trip);
        return Task.CompletedTask;
    }

    public async Task AddAsync(Trip trip, CancellationToken cancellationToken = default)
        => await _dbContext.Trips.AddAsync(trip, cancellationToken);
}