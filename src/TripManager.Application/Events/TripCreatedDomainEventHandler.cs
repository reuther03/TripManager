using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Common.Primitives.DomainEvents;
using TripManager.Domain.DomainEvents;
using TripManager.Domain.Emails;
using TripManager.Domain.Users;

namespace TripManager.Application.Events;

public class TripCreatedDomainEventHandler : IDomainEventHandler<TripCreatedDomainEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ITripDbContext _tripDbContext;
    private readonly IUserContext _userContext;

    public TripCreatedDomainEventHandler(IEmailSender emailSender, IUserContext userContext, ITripDbContext tripDbContext)
    {
        _emailSender = emailSender;
        _userContext = userContext;
        _tripDbContext = tripDbContext;
    }

    public async Task Handle(TripCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = _tripDbContext.Users
            .Where(x => x.Id == UserId.From(notification.UserId))
            .Select(x => x.Email)
            .FirstOrDefault();

        if (email is null)
            return;

        var message = new EmailMessage(email, "Trip Created", "<p>Trip with id " + notification.TripId + " has been created.</p>");
        await _emailSender.Send(message);
    }
}