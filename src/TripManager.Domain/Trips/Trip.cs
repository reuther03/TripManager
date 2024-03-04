using TripManager.Common.Exceptions.Domain;
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
    public TripSettings Settings { get; private set; } = null!;
    public IReadOnlyList<TripActivity> Activities => _activities.AsReadOnly();
    public UserId UserId { get; private set; }

    private Trip()
    {
    }

    private Trip(TripId id, Country country, Description description, Date start, Date end, TripSettings settings, UserId userId)
        : base(id)
    {
        Country = country;
        Description = description;
        Start = start;
        End = end;
        Settings = settings;
        UserId = userId;
    }

    public static Trip Create(Country country, Description description, Date start, Date end, TripSettings settings, UserId userId)
    {
        if (start.Value.Date > end.Value.Date)
        {
            throw new DomainException("Start date cannot be greater than end date");
        }

        return new Trip(TripId.New(), country, description, start, end, settings, userId);
    }

    public void AddActivity(TripActivity activity)
    {
        _activities.Add(activity);
    }

    public void Update(Country country, Description description, Date start, Date end, TripSettings settings)
    {
        if (start.Value.Date > end.Value.Date)
        {
            throw new DomainException("Start date cannot be greater than end date");
        }

        Country = country;
        Description = description;
        Start = start;
        End = end;
        Settings = settings;
    }

    public void RemoveActivity(Guid activityId)
    {
        var activity = _activities.SingleOrDefault(x => x.Id == TripActivityId.From(activityId));
        if (activity is null)
        {
            return;
        }

        _activities.Remove(activity);
    }

    public TripActivity? UpdateActivity(Guid requestActivityId, string requestName, string requestDescription, Date date, Date date1, Location location)
    {
        var activity = _activities.SingleOrDefault(x => x.Id == TripActivityId.From(requestActivityId));
        activity?.Update(requestName, requestDescription, date, date1, location);
        return activity;
    }
}