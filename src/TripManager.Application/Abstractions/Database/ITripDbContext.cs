using Microsoft.EntityFrameworkCore;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.Activities;
using TripManager.Domain.Users;

namespace TripManager.Application.Abstractions.Database;

public interface ITripDbContext
{
    DbSet<User> Users { get; }
    DbSet<Trip> Trips { get; }
    DbSet<TripActivity> TripActivities { get; }
}