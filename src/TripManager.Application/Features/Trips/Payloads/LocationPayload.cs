using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Features.Trips.Payloads;

public class LocationPayload
{
    public string LocationAddress { get; init; }
    public string LocationCoordinates { get; init; }

    public Location ToLocation() => new(LocationAddress, LocationCoordinates);
}