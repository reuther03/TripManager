using MediatR;
using TripManager.Common.Primitives.DomainEvents;
using TripManager.Domain.DomainEvents;

namespace TripManager.Application.Events;

internal sealed class UpdatedTripDomainEventHandler : IDomainEventHandler<UpdatedTripDomainEvent>
{
    private readonly ISender _sender;

    public UpdatedTripDomainEventHandler(ISender sender)
    {
        _sender = sender;
    }

    public Task Handle(UpdatedTripDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}