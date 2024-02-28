using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Features.Trips;
using TripManager.Application.Features.Trips.Commands;

namespace TripManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ISender _sender;

    public ActivityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(CreateActivityCommand command)
    {
        var activity = await _sender.Send(command);
        return Ok(activity);
    }
}