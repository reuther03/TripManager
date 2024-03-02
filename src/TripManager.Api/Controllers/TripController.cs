using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Features.Trips;
using TripManager.Application.Features.Trips.Commands;
using TripManager.Application.Features.Trips.Queries;
using TripManager.Application.Features.Trips.Queries.GetTrip;
using TripManager.Domain.Trips;

namespace TripManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController : ControllerBase
{
    private readonly ISender _sender;

    public TripsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(CreateTripCommand command)
    {
        var trip = await _sender.Send(command);
        return Ok(trip);
    }

    [HttpPost("{id:guid}/activities")]
    public async Task<ActionResult<Guid>> Post(Guid id, AddActivityCommand command)
    {
        var activity = await _sender.Send(command with{TripId = id});
        return Ok(activity);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Trip>> Get(Guid id)
    {
        var trip = await _sender.Send(new GetTripQuery(id));
        return Ok(trip);
    }
}