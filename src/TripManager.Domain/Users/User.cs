using TripManager.Common.Primitives;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips;
using TripManager.Domain.Users.ValueObjects;

namespace TripManager.Domain.Users;

public class User : Entity<UserId>
{
    private readonly List<Trip> _trips = [];

    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Fullname Fullname { get; private set; }
    public Role Role { get; private set; }
    public Date CreatedAt { get; private set; }

    public IReadOnlyCollection<Trip> Trips => _trips.AsReadOnly();

    private User()
    {
    }

    private User(UserId id, Email email, Username username, Password password, Fullname fullname, Role role, Date createdAt) : base(id)
    {
        Email = email;
        Username = username;
        Password = password;
        Fullname = fullname;
        Role = role;
        CreatedAt = createdAt;
    }

    internal static User CreateAdmin(Email email, Username username, Password password, Fullname fullname)
        => new(UserId.New(), email, username, password, fullname, Role.Admin, Date.Now);

    internal static User CreateUser(Email email, Username username, Password password, Fullname fullname)
        => new(UserId.New(), email, username, password, fullname, Role.User, Date.Now);
}