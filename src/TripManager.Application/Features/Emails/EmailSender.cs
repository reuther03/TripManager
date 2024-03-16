using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using TripManager.Application.Abstractions;
using TripManager.Domain.Emails;

namespace TripManager.Application.Features.Emails;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _options;

    public EmailSender(IOptions<EmailSettings> options)
    {
        _options = options.Value;
    }

    public async Task Send(EmailMessage request)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.Username, _options.FromAddress));
        message.To.Add(new MailboxAddress("", request.Email));
        message.Subject = request.Subject;
        message.Body = new TextPart("plain") { Text = request.Body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_options.FromAddress, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}