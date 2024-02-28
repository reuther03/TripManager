using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Abstractions.Database.Repositories;

public interface IActivityRepository
{
    Task AddAsync(TripActivity activity, CancellationToken cancellationToken = default);
}