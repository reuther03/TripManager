using TripManager.Common.Primitives;
using TripManager.Common.Primitives.Domain;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips.Activities;
using TripManager.Domain.Trips.ValueObjects;
using TripManager.Domain.Users;

namespace TripManager.Domain.Trips;

public class Trip : Entity<TripId>
{
    private readonly List<TripActivity> _activities = [];

    public Country Country { get; private set; }
    public Description Description { get; private set; }
    public Date Start { get; private set; }
    public Date End { get; private set; }
    public TripSettings Settings { get; private set; }
    public IReadOnlyList<TripActivity> Activities => _activities.AsReadOnly();
    public UserId UserId { get; private set; }

    private Trip()
    {
    }

    private Trip(TripId id, Country country, Description description, Date start, Date end, UserId userId)
        : base(id)
    {
        Country = country;
        Description = description;
        Start = start;
        End = end;
        UserId = userId;
    }

    public static Trip CreateInstance(Country country, Description description, Date start, Date end, UserId userId)
    {
        return new Trip(TripId.New(), country, description, start, end, userId);
    }
}