using TripManager.Common.Primitives;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trip.ValueObjects;

namespace TripManager.Domain.Trip;

public class Trip : Entity<TripId>
{
    public Country Country { get; private set; }
    public Description Description { get; private set; }
    public Date StartDate { get; private set; }
    public Date EndDate { get; private set; }

}