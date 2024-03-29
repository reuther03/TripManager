﻿using TripManager.Common.Primitives.Domain;
using TripManager.Common.ValueObjects;
using TripManager.Domain.DomainEvents;
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

    public static User CreateAdmin(Email email, Username username, Password password, Fullname fullname)
        => new(UserId.New(), email, username, password, fullname, Role.Admin, Date.Now.Value);

    public static User CreateUser(Email email, Username username, Password password, Fullname fullname)
    {
        var user = new User(UserId.New(), email, username, password, fullname, Role.User, Date.Now);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email));
        return user;
    }
}