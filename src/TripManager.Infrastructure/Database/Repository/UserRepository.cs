using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Domain.Users;
using TripManager.Domain.Users.ValueObjects;

namespace TripManager.Infrastructure.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly TripDbContext _context;

    public UserRepository(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public Task<bool> ExistsWithUsernameAsync(Username username, CancellationToken cancellationToken = default)
        => _context.Users.AnyAsync(x => x.Username == username, cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetByUsernameAsync(string username)
        => await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await _context.Users.AddAsync(user, cancellationToken);
}