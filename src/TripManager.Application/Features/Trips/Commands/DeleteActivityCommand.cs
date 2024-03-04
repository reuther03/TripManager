using MediatR;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;

namespace TripManager.Application.Features.Trips.Commands;

public record DeleteActivityCommand(Guid TripId, Guid ActivityId) : ICommand<Unit>
{
    internal sealed class Handler : ICommandHandler<DeleteActivityCommand, Unit>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.TripId, cancellationToken)
                ?? throw new ApplicationValidationException("Trip not found");

            trip.RemoveActivity(request.ActivityId);

            _tripRepository.DeleteActivity(request.TripId, request.ActivityId, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}