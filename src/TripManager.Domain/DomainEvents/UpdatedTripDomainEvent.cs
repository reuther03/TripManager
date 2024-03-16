using TripManager.Common.Primitives.DomainEvents;

namespace TripManager.Domain.DomainEvents;

public sealed record UpdatedTripDomainEvent(Guid TripId) : IDomainEvent;