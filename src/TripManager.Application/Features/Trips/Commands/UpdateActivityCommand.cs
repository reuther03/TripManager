﻿using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Application.Features.Trips.Queries.GetTrip;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Features.Trips.Commands;

public record UpdateActivityCommand(
    Guid TripId,
    Guid ActivityId,
    string Name,
    string Description,
    DateTimeOffset Start,
    DateTimeOffset End,
    string LocationAddress,
    string LocationCoordinates) : ICommand<TripActivityDto?>
{
    internal sealed class Handler : ICommandHandler<UpdateActivityCommand, TripActivityDto?>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TripActivityDto?> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.TripId, cancellationToken)
                ?? throw new ApplicationValidationException("Trip not found");

            var updatedActivity = trip.UpdateActivity(
                request.ActivityId,
                request.Name,
                request.Description,
                new Date(request.Start),
                new Date(request.End),
                new Location(request.LocationAddress, request.LocationCoordinates));

            await _unitOfWork.CommitAsync(cancellationToken);
            return TripActivityDto.AsNullableDto(updatedActivity);
        }
    }
}