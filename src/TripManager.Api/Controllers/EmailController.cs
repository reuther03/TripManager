using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Abstractions;
using TripManager.Domain.Emails;

namespace TripManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;

    public EmailController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public async Task<ActionResult> SendEmail(EmailMessage request)
    {
        await _emailSender.Send(request);
        return Ok();
    }
}