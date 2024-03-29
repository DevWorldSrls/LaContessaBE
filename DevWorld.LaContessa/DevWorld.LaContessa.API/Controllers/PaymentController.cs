﻿using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Query.Abstractions.Stripe;
using DevWorld.LaContessa.Query.Abstractions.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Authorize]
[Route("payment")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("card")]
    public async Task<ActionResult<GetCard.Response>> GetCard(Guid userId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetCard(
                userId
            ),
            cancellationToken
        );
    }

    [HttpPost("card")]
    public async Task<ActionResult> AddCard([FromBody] CreateCard.StripeCustomer customer,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateCard
            {
                Customer = customer
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpDelete("card")]
    public async Task<ActionResult> DeleteCard(Guid userId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteCard
            {
                UserId = userId
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPost("refund")]
    public async Task<ActionResult> Refund(Guid bookingId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new RefundRequest
            {
                BookingId = bookingId
            },
            cancellationToken
        );

        return Ok();
    }
}
