using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives;
using TripManager.Common.Primitives.Domain;

namespace TripManager.Common.ValueObjects;

public record Description : ValueObject
{
    public string Value { get; }

    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Description cannot be empty.");
        }

        Value = value;
    }

    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string description) => new(description);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}