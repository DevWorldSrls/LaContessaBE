using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Route("activties")]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<ActionResult> CreateActivity([FromBody] CreateActivity.ActivityDetail activity, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateActivity
            {
                Activity = activity,
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateActivity([FromBody] UpdateActivity.ActivityDetail activity, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateActivity
            {
                Activity = activity,
            },
            cancellationToken
        );

        return Ok();
    }
}