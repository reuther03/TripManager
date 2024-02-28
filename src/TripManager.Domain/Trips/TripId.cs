using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives.Domain;

namespace TripManager.Domain.Trips;

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

    public static TripId New() => new(Guid.NewGuid());
    public static TripId From(Guid value) => new(value);
    public static TripId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(TripId tripId) => tripId.Value;
    public static implicit operator TripId(Guid tripId) => new(tripId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}