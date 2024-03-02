using Microsoft.EntityFrameworkCore;
using TripManager.Application.Abstractions.Database;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Queries.GetTrip;

public record GetTripQuery(Guid Id) : IQuery<TripDto>
{
    internal sealed class Handler : IQueryHandler<GetTripQuery, TripDto>
    {
        private readonly ITripDbContext _dbContext;

        public Handler(ITripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TripDto> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            var trip = await _dbContext.Trips
                .Include(x => x.Activities)
                .SingleOrDefaultAsync(x => x.Id == TripId.From(request.Id), cancellationToken);

            if (trip is null)
            {
                throw new ApplicationValidationException("Trip not found.");
            }

            return TripDto.AsDto(trip);
        }
    }
}