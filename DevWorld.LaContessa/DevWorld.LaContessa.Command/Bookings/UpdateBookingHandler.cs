using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Bookings;

public class UpdateBookingHandler : IRequestHandler<UpdateBooking>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IMediator _mediator;

    public UpdateBookingHandler(
        LaContessaDbContext laContessaDbContext,
        IMediator mediator
        )
    {
        _laContessaDbContext = laContessaDbContext;
        _mediator = mediator;
    }

    public async Task Handle(UpdateBooking request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _laContessaDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == request.Booking.Id && !x.IsDeleted, cancellationToken) ?? throw new BookingNotFoundException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == new Guid(request.Booking.UserId), cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == Guid.Parse(request.Booking.ActivityId), cancellationToken) ?? throw new ActivityNotFoundException();

        bookingToUpdate.User = user;
        bookingToUpdate.Date = request.Booking.Date;
        bookingToUpdate.BookingName = request.Booking.BookingName;
        bookingToUpdate.PhoneNumber = request.Booking.PhoneNumber;
        bookingToUpdate.Activity = activity;
        bookingToUpdate.IsLesson = request.Booking.IsLesson;
        bookingToUpdate.TimeSlot = request.Booking.TimeSlot;
        bookingToUpdate.Status = request.Booking.Status;
        bookingToUpdate.BookingPrice = request.Booking.BookingPrice;
        bookingToUpdate.PaymentPrice = request.Booking.PaymentPrice;

        if (bookingToUpdate.Status == Domain.Enums.BookingStatus.Payed && bookingToUpdate.PaymentPrice != null && user.CustomerId != null)
        {
            await _mediator.Send(
                new CreateStripePaymentRequest
                {
                    CustomerId = user.CustomerId!,
                    Amount = bookingToUpdate.PaymentPrice ?? 0,
                    Currency = "EUR",
                    Description = "Pagamento " + activity.Name,
                    ReceiptEmail = "info@lacontessa.it",
                },
                cancellationToken
            );
        }

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
