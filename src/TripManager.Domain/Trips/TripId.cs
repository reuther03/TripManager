using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives;
using TripManager.Domain.Users;

namespace TripManager.Domain.Trip;

public record TripId : ValueObject
{
    public Guid Value { get; }

    public TripId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Trip id cannot be empty.");
        }

        Value = value;
    }

    public static UserId New() => new(Guid.NewGuid());
    public static UserId From(Guid value) => new(value);
    public static UserId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(TripId userId) => userId.Value;
    public static implicit operator TripId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}