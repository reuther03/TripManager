using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives.Domain;

namespace TripManager.Domain.Trips.Activities;

public record TripActivityId : ValueObject
{
    public Guid Value { get; }

    public TripActivityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Trip id cannot be empty.");
        }

        Value = value;
    }

    public static TripActivityId New() => new(Guid.NewGuid());
    public static TripActivityId From(Guid value) => new(value);
    public static TripActivityId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(TripActivityId tripActivityId) => tripActivityId.Value;
    public static implicit operator TripActivityId(Guid tripActivityId) => new(tripActivityId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}