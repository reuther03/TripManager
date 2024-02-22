using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives;

namespace TripManager.Domain.Trips;

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