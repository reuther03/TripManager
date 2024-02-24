using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives;
using TripManager.Common.Primitives.Domain;

namespace TripManager.Domain.Trips.ValueObjects;

public record Country : ValueObject
{
    public string Value { get; set; }

    public Country(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Country cannot be empty");
        }

        Value = value;
    }

    public static implicit operator Country(string value) => new(value);
    public static implicit operator string(Country country) => country.Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}