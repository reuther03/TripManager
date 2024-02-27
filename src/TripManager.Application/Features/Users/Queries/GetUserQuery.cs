using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Features.Users.Dto;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Domain.Users;

namespace TripManager.Application.Features.Users.Queries;

public record GetUserQuery(Guid Id) : IQuery<UserDto>
{
    internal sealed class Handler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ITripDbContext _dbContext;

        public Handler(ITripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Where(x => x.Id == UserId.From(request.Id))
                .Select(x => UserDto.AsDto(x))
                .SingleOrDefaultAsync(cancellationToken);

            return user ?? throw new ApplicationValidationException("User not found.");
        }
    }
}