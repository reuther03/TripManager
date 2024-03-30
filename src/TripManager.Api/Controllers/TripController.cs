using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripManager.Api.Controllers.Base;
using TripManager.Application.Features.Trips.Commands;
using TripManager.Application.Features.Trips.Commands.CreateTrip;
using TripManager.Application.Features.Trips.Queries.GetAllTrips;
using TripManager.Application.Features.Trips.Queries.GetTrip;

namespace TripManager.Api.Controllers;

public class TripsController : BaseController
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
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var trip = await _sender.Send(new GetTripQuery(id));
        return HandleResult(trip);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTripsQuery query)
    {
        var trips = await _sender.Send(query);
        return HandleResult(trips);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command)
    {
        var trip = await _sender.Send(command);
        return HandleResult(trip);
    }

    [HttpPost("{id:guid}/activities")]
    [Authorize]
    public async Task<IActionResult> AddActivity([FromRoute] Guid id, [FromBody] AddActivityCommand command)
    {
        var activity = await _sender.Send(command with { TripId = id });
        return HandleResult(activity);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateTrip([FromRoute] Guid id, [FromBody] UpdateTripCommand command)
    {
        await _sender.Send(command with { Id = id });
        return HandleResult();
    }

    [HttpPut("{id:guid}/activities/{activityId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateActivity([FromRoute] Guid id, [FromRoute] Guid activityId, UpdateActivityCommand command)
    {
        await _sender.Send(command with { TripId = id, ActivityId = activityId });
        return HandleResult();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteTrip([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteTripCommand(id));
        return NoContent();
    }

    [HttpDelete("{id:guid}/activities/{activityId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteActivity([FromRoute] Guid id, [FromRoute] Guid activityId)
    {
        await _sender.Send(new DeleteActivityCommand(id, activityId));
        return NoContent();
    }
}