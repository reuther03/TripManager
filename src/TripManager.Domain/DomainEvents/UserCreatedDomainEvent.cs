using TripManager.Common.Primitives.DomainEvents;

namespace TripManager.Domain.DomainEvents;

public sealed record UserCreatedDomainEvent(Guid UserId, string Email) : IDomainEvent;