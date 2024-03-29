using TripManager.Domain.Trips;

namespace TripManager.Application.Features.Trips.Queries.GetTrip;

public class TripDto
{
    public Guid Id { get; init; }
    public string Country { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
    public string SettingsDescription { get; init; } = null!;
    public decimal SettingsBudget { get; init; }
    public List<TripActivityDto> Activities { get; init; } = null!;

    public static TripDto AsDto(Trip trip)
    {
        return new TripDto
        {
            Id = trip.Id,
            Country = trip.Country,
            Description = trip.Description,
            Start = trip.Start,
            End = trip.End,
            SettingsDescription = trip.Settings.Description,
            SettingsBudget = trip.Settings.Budget,
            Activities = trip.Activities.Select(TripActivityDto.AsDto).ToList()
        };
    }

    // public static Expression<Func<Trip, TripDto>> Mapper => trip => new TripDto
    // {
    //     Id = trip.Id,
    //     Country = trip.Country,
    //     Description = trip.Description,
    //     Start = trip.Start,
    //     End = trip.End,
    //     SettingsDescription = trip.Settings.Description,
    //     SettingsBudget = trip.Settings.Budget,
    //     Activities = trip.Activities.Select(TripActivityDto.AsDto).ToList()
    // };
}