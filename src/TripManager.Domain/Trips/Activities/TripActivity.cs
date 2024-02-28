using TripManager.Common.Primitives.Domain;
using TripManager.Common.ValueObjects;

namespace TripManager.Domain.Trips.Activities;

public class TripActivity : Entity<TripActivityId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Date Start { get; private set; }
    public Date End { get; private set; }
    public Location Location { get; private set; }
    public TripId TripId { get; private set; }

    private TripActivity()
    {
    }

    private TripActivity(TripActivityId id, string name, string description, Date start, Date end, Location location)
        : base(id)
    {
        Name = name;
        Description = description;
        Start = start;
        End = end;
        Location = location;
    }

    public static TripActivity Create(string name, string description, Date start, Date end, Location location)
    {
        return new TripActivity(TripActivityId.New(), name, description, start, end, location);
    }


}