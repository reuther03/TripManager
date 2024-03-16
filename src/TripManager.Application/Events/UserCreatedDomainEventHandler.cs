using TripManager.Application.Abstractions;
using TripManager.Common.Primitives.DomainEvents;
using TripManager.Domain.DomainEvents;
using TripManager.Domain.Emails;

namespace TripManager.Application.Events;

public sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
    private readonly IEmailSender _emailSender;

    public UserCreatedDomainEventHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var message = new EmailMessage(
            notification.Email,
            " ",
            """
            <div style="background-color: #f0f0f0; padding: 20px; font-family: Arial, sans-serif; line-height: 1.5;">
                <div style="max-width: 600px; margin: auto; background: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);">
                    <h2 style="color: #333333; text-align: center;">Welcome to Trip Manager!</h2>
                    <p style="color: #555555;">Hello there,</p>
                    <p style="color: #555555;">Thank you for signing up for Trip Manager. We're thrilled to have you on board and can't wait for you to explore all the features we offer.</p>
                    <p style="color: #555555;">Here are some resources to get you started:</p>
                    <ul style="color: #555555;">
                        <li>User Guide</li>
                        <li>FAQs</li>
                        <li>Support Forum</li>
                    </ul>
                    <p style="color: #555555;">If you have any questions, feel free to reply to this email or reach out to our support team. We're here to help!</p>
                    <div style="text-align: center; margin-top: 20px;">
                        <a href="https://github.com/reuther03" style="display: inline-block; background-color: #007bff; color: #ffffff; padding: 10px 20px; text-decoration: none; border-radius: 5px;">Visit Our Website</a>
                    </div>
                </div>
            </div>
            """);
        await _emailSender.Send(message);
    }
}