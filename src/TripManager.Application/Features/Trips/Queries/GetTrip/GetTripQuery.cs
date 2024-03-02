using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Queries.GetTrip;

public record GetTripQuery(Guid Id) : IQuery<Trip>
{
    internal sealed class Handler : IQueryHandler<GetTripQuery, Trip>
    {
        private readonly ITripRepository _tripRepository;

        public Handler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<Trip> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.Id, cancellationToken);

            return trip ?? throw new ApplicationValidationException("Trip not found.");
        }
    }
}