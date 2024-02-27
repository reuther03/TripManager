using TripManager.Domain.Trips;
using TripManager.Domain.Users;

namespace TripManager.Application.Abstractions.Database.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync(UserId id, CancellationToken cancellationToken = default);
    Task AddAsync(Trip trip, CancellationToken cancellationToken = default);
}