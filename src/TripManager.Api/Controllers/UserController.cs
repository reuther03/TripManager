using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Features.Users.Commands;

namespace TripManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(SignUpCommand command)
    {
        var user = await _sender.Send(command);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var token = await _sender.Send(command, cancellationToken);
        return Ok(token);
    }
}