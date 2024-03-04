using TripManager.Domain.Trips.Activities;

namespace TripManager.Application.Features.Trips.Queries.GetTrip;

public class TripActivityDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
    public string? LocationAddress { get; init; }
    public string? LocationCoordinates { get; init; }

    public static TripActivityDto AsDto(TripActivity tripActivity)
    {
        return new TripActivityDto
        {
            Name = tripActivity.Name,
            Description = tripActivity.Description,
            Start = tripActivity.Start,
            End = tripActivity.End,
            LocationAddress = tripActivity.Location.Address,
            LocationCoordinates = tripActivity.Location.Coordinates
        };
    }
}