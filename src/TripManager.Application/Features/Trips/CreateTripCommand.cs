﻿using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.ValueObjects;

namespace TripManager.Application.Features.Trips;

public record CreateTripCommand(
    string Country,
    string Description,
    DateTimeOffset Start,
    DateTimeOffset End,
    string SettingsDescription,
    decimal SettingsBudget
) : ICommand<Trip>
{
    internal sealed class Handler : ICommandHandler<CreateTripCommand, Trip>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserContext userContext, IUserRepository userRepository, ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Trip> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            if (!_userContext.IsAuthenticated)
                throw new ApplicationValidationException("User not authenticated");

            var user = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken)
                ?? throw new ApplicationValidationException("User not found");

            var trip = Trip.Create(
                new Country(request.Country),
                new Description(request.Description),
                new Date(request.Start),
                new Date(request.End),
                new TripSettings(request.SettingsDescription, request.SettingsBudget),
                user.Id);

            await _tripRepository.AddAsync(trip, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return trip;
        }
    }
}