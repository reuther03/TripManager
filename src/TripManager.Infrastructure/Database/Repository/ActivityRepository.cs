using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Domain.Trips.Activities;

namespace TripManager.Infrastructure.Database.Repository;

public class ActivityRepository : IActivityRepository
{
    private readonly TripDbContext _dbContext;

    public ActivityRepository(TripDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TripActivity?> GetByIdAsync(TripActivityId id, CancellationToken cancellationToken = default)
        => await _dbContext.TripActivities
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);


    public async Task AddAsync(TripActivity activity, CancellationToken cancellationToken = default)
        => await _dbContext.TripActivities.AddAsync(activity, cancellationToken);
}