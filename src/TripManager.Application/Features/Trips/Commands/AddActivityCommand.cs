using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Application.Features.Trips.Payloads;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Features.Trips.Commands;

public record AddActivityCommand(
    string Name,
    string Description,
    DateTimeOffset Start,
    DateTimeOffset End,
    LocationPayload Location,
    Guid TripId
) : ICommand<TripActivity>
{
    internal sealed class Handler : ICommandHandler<AddActivityCommand, TripActivity>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITripRepository tripRepository, IUnitOfWork unitOfWork, IActivityRepository activityRepository)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
            _activityRepository = activityRepository;
        }

        public async Task<TripActivity> Handle(AddActivityCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.TripId, cancellationToken)
                ?? throw new ApplicationValidationException("Trip not found");

            if (trip.Start.DateOnly() > request.Start || trip.End.DateOnly() < request.End)
                throw new ApplicationValidationException("Activity dates are not within trip dates");

            var activity = TripActivity.Create(
                request.Name,
                request.Description,
                new Date(request.Start),
                new Date(request.End),
                request.Location.ToLocation());

            trip.AddActivity(activity);

            await _activityRepository.AddAsync(activity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return activity;
        }
    }
}