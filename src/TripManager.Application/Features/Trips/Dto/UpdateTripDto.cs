using TripManager.Application.Features.Trips.Queries.GetTrip;
using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Dto;

public class UpdateTripDto
{
    public string Country { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
    public string SettingsDescription { get; init; } = null!;
    public decimal SettingsBudget { get; init; }

    public static UpdateTripDto AsDto(Trip trip)
    {
        return new UpdateTripDto
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