using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Payloads;

public class UpdateTripPayload
{
    public string Country { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
    public string SettingsDescription { get; init; } = null!;
    public decimal SettingsBudget { get; init; }

    public static UpdateTripPayload AsDto(Trip trip)
    {
        return new UpdateTripPayload
        {
            Country = trip.Country,
            Description = trip.Description,
            Start = trip.Start,
            End = trip.End,
            SettingsDescription = trip.Settings.Description,
            SettingsBudget = trip.Settings.Budget,
        };
    }
}