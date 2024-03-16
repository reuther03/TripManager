using TripManager.Domain.Emails;

namespace TripManager.Application.Abstractions;

public interface IEmailSender
{
    public Task Send(EmailMessage request);
}