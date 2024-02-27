using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Features.Users.Commands;

namespace TripManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
// [Tags] ⬅️ zmienia nazwę sekcji w swagerze
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> SignUp(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _sender.Send(command, cancellationToken);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken = default)
    {
        var token = await _sender.Send(command, cancellationToken);
        return Ok(token);
    }
}