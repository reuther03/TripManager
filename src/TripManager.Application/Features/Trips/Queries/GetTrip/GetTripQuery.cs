using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Queries.GetTrip;

public record GetTripQuery(Guid Id) : IQuery<TripDto>
{
    internal sealed class Handler : IQueryHandler<GetTripQuery, TripDto>
    {
        private readonly ITripRepository _tripRepository;

        public Handler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<TripDto> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.Id, cancellationToken);

            if (trip is null)
                throw new ApplicationException($"Trip with id {request.Id} was not found.");

            return TripDto.AsDto(trip);
        }
    }
}