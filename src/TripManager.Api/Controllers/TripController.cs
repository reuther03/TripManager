using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripManager.Application.Features.Trips.Commands;
using TripManager.Application.Features.Trips.Queries.GetAllTrips;
using TripManager.Application.Features.Trips.Queries.GetTrip;
using TripManager.Common.Primitives.Pagination;
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

    /// <summary>
    /// Gets a trip by its id
    /// </summary>
    /// <remarks>
    /// Example request:
    /// ```
    /// GET /trips/123e4567-e89b-12d3-a456-426614174000
    /// ```
    /// </remarks>
    /// <param name="id">The id of the trip</param>
    /// <returns>The trip</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Trip>> GetById([FromRoute] Guid id)
    {
        var trip = await _sender.Send(new GetTripQuery(id));
        return Ok(trip);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<TripDto>>> GetAll([FromQuery] GetAllTripsQuery query)
    {
        var trips = await _sender.Send(query);
        return Ok(trips);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTrip([FromBody] CreateTripCommand command)
    {
        var trip = await _sender.Send(command);
        return Ok(trip);
    }

    [HttpPost("{id:guid}/activities")]
    public async Task<ActionResult<Guid>> AddActivity([FromRoute] Guid id, [FromBody] AddActivityCommand command)
    {
        var activity = await _sender.Send(command with { TripId = id });
        return Ok(activity);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateTrip([FromRoute] Guid id, [FromBody] UpdateTripCommand command)
    {
        await _sender.Send(command with { Id = id });
        return Ok();
    }

    [HttpPut("{id:guid}/activities/{activityId:guid}")]
    public async Task<ActionResult> UpdateActivity([FromRoute] Guid id, [FromRoute] Guid activityId, UpdateActivityCommand command)
    {
        await _sender.Send(command with { TripId = id, ActivityId = activityId });
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteTrip([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteTripCommand(id));
        return NoContent();
    }

    [HttpDelete("{id:guid}/activities/{activityId:guid}")]
    public async Task<ActionResult> DeleteActivity([FromRoute] Guid id, [FromRoute] Guid activityId)
    {
        await _sender.Send(new DeleteActivityCommand(id, activityId));
        return NoContent();
    }
}