using MediatR;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;

namespace TripManager.Application.Features.Trips.Commands;

public record DeleteTripCommand(Guid TripId) : ICommand<Unit>
{
    internal sealed class Handler : ICommandHandler<DeleteTripCommand, Unit>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.TripId, cancellationToken)
                ?? throw new ApplicationValidationException("Trip not found");

            _tripRepository.Delete(trip, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}