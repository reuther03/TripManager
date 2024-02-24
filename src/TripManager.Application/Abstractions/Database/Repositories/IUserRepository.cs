using TripManager.Domain.Users;
using TripManager.Domain.Users.ValueObjects;

namespace TripManager.Application.Abstractions.Database.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithUsernameAsync(Username username, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}