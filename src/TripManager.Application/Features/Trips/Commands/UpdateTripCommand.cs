using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.ValueObjects;

namespace TripManager.Application.Features.Trips.Commands;

public record UpdateTripCommand(
    Guid Id,
    string Country,
    string Description,
    DateTimeOffset Start,
    DateTimeOffset End,
    string SettingsDescription,
    decimal SettingsBudget
) : ICommand<Trip>
{
    internal sealed class Handler : ICommandHandler<UpdateTripCommand, Trip>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Trip> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(new TripId(request.Id), cancellationToken)
                ?? throw new ApplicationValidationException("Trip not found");

            trip.Update(
                new Country(request.Country),
                new Description(request.Description),
                new Date(request.Start),
                new Date(request.End),
                new TripSettings(request.SettingsDescription, request.SettingsBudget));

            await _tripRepository.AddAsync(trip, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return trip;
        }
    }
}