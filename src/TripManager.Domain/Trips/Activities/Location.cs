using TripManager.Common.Primitives.Domain;

namespace TripManager.Domain.Trips.Activities;

public record Location : ValueObject
{
    public string Address { get; }
    public string Coordinates { get; }

    public Location(string address, string coordinates)
    {
        Address = address;
        Coordinates = coordinates;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Address;
        yield return Coordinates;
    }
}