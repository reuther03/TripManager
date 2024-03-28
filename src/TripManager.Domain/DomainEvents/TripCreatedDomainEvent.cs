using TripManager.Common.Primitives.DomainEvents;

namespace TripManager.Domain.DomainEvents;

public sealed record TripCreatedDomainEvent(Guid TripId, Guid UserId) : IDomainEvent;