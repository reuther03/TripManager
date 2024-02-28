using TripManager.Common.Primitives.Domain;
using TripManager.Common.ValueObjects;

namespace TripManager.Domain.Trips;

public record TripSettings : ValueObject
{
    public Description Description { get; }
    public decimal Budget { get; }

    public TripSettings(Description description, decimal budget)
    {
        Description = description;
        Budget = budget;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Description;
        yield return Budget;
    }
}