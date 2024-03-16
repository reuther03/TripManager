namespace TripManager.Domain.Emails;

public class EmailMessage
{
    public string Email { get; }
    public string Subject { get; }
    public string Body { get; }

    public EmailMessage(string email, string subject, string body)
    {
        Email = email;
        Subject = subject;
        Body = body;
    }
}