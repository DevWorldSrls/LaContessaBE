using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Authorize]
[Route("subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetSubscriptions.Response>> GetSubscriptions(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetSubscriptions(),
            cancellationToken
        );
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetSubscription.Response>> GetSubscription(Guid id,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetSubscription(id),
            cancellationToken
        );
    }

    [HttpPost]
    public async Task<ActionResult> CreateSubscription([FromBody] CreateSubscription.SubscriptionDetail subscription,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateSubscription
            {
                Subscription = subscription
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateSubscription([FromBody] UpdateSubscription.SubscriptionDetail subscription,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateSubscription
            {
                Subscription = subscription
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpGet("userId")]
    public async Task<ActionResult<GetSubscriptionByUserId.Response>> GetSubscription(string userId,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetSubscriptionByUserId(userId),
            cancellationToken
        );
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteSubscription(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteSubscription(id),
            cancellationToken
        );

        return Ok();
    }
}