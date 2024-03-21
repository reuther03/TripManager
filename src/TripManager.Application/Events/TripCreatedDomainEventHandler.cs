using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Common.Primitives.DomainEvents;
using TripManager.Domain.DomainEvents;
using TripManager.Domain.Emails;

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
            .Where(x => x.Id == _userContext.UserId)
            .Select(x => x.Email)
            .FirstOrDefault();

        var message = new EmailMessage(email, " ", "<p>Trip is created!!!</p>");
        await _emailSender.Send(message);
    }
}