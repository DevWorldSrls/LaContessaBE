using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Query.Abstractions.Activities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Authorize]
[Route("activities")]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves activities.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response containing activities.</returns>
    [HttpGet]
    public async Task<ActionResult<GetActivities.Response>> GetActivities(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetActivities(),
            cancellationToken
        );
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetActivity.Response>> GetActivity(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetActivity(id),
            cancellationToken
        );
    }

    [HttpPost]
    public async Task<ActionResult> CreateActivity([FromBody] CreateActivity.ActivityDetail activity,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateActivity
            {
                Activity = activity
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateActivity([FromBody] UpdateActivity.ActivityDetail activity,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateActivity
            {
                Activity = activity
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteActivity(id),
            cancellationToken
        );

        return Ok();
    }
}