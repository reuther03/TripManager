using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Api.Controllers.Base;
using TripManager.Application.Features.Users.Commands.Login;
using TripManager.Application.Features.Users.Commands.SignUp;

namespace TripManager.Api.Controllers;

// [Tags] ⬅️ zmienia nazwę sekcji w swagerze
public class UsersController : BaseController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken = default)
    {
        var token = await _sender.Send(command, cancellationToken);
        return HandleResult(token);
    }
}