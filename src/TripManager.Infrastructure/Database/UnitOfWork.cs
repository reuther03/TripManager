using TripManager.Application.Abstractions.Database;

namespace TripManager.Infrastructure.Database;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TripDbContext _tripDbContext;

    public UnitOfWork(TripDbContext tripDbContext)
    {
        _tripDbContext = tripDbContext;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await _tripDbContext.SaveChangesAsync(cancellationToken) > 0;
        return result;
    }
}