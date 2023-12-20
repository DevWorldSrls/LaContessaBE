using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Route("bookings")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetBookings.Response>> GetBookings(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetBookings(),
            cancellationToken
        );
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetBooking.Response>> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetBooking(id),
            cancellationToken
        );
    }

    [HttpPost]
    public async Task<ActionResult> CreateBooking([FromBody] CreateBooking.BookingDetail booking,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateBooking
            {
                Booking = booking
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBooking([FromBody] UpdateBooking.BookingDetail booking,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateBooking
            {
                Booking = booking
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpGet("userId")]
    public async Task<ActionResult<GetBookingByUserId.Response>> GetBooking(string userId,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetBookingByUserId(userId),
            cancellationToken
        );
    }
}