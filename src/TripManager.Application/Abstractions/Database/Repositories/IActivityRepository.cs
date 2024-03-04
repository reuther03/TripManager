using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Abstractions.Database.Repositories;

public interface IActivityRepository
{
    Task<TripActivity?> GetByIdAsync(TripActivityId id, CancellationToken cancellationToken = default);
    Task AddAsync(TripActivity activity, CancellationToken cancellationToken = default);
}