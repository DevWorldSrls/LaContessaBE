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
            .FirstOrDefaultAsync(u => u.Id == request.Booking.UserId, cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == request.Booking.ActivityId, cancellationToken) ?? throw new ActivityNotFoundException();

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

        switch (bookingToUpdate.Status)
        {
            case Domain.Enums.BookingStatus.Waiting:
            case Domain.Enums.BookingStatus.Confirmed:
                break;
            case Domain.Enums.BookingStatus.Cancelled:
                var activityDateAPS = activity.DateList.FirstOrDefault(x => x.Date == bookingToUpdate.Date) ?? throw new ActivityNotFoundException();
                var timeSlotToUpdateAPS = activityDateAPS.TimeSlotList.FirstOrDefault(x => x.TimeSlot == bookingToUpdate.TimeSlot) ?? throw new ActivityNotFoundException();
                timeSlotToUpdateAPS.IsAlreadyBooked = false;
                bookingToUpdate.PaymentIntentId = null;

                break;
            case Domain.Enums.BookingStatus.Payed:
                if (bookingToUpdate.PaymentPrice is not null)
                {
                    var paymentAmount = bookingToUpdate.PaymentPrice ?? 0;

                    if (bookingToUpdate.PaymentPrice != bookingToUpdate.BookingPrice)
                    {
                        paymentAmount = bookingToUpdate.BookingPrice - bookingToUpdate.PaymentPrice ?? 0;

                        await _mediator.Send(
                            new CreateStripePaymentRequest
                            {
                                CustomerId = user.CustomerId ?? throw new CustomerNotFoundException(),
                                PaymentMethodId = user.PaymentMethodId ?? throw new PaymentMethodNotFoundException(),
                                Amount = paymentAmount,
                                Currency = "EUR",
                                Description = "Pagamento parte restante della Prenotazione di: " + activity.Name,
                                ReceiptEmail = "info@lacontessa.it",
                            },
                            cancellationToken
                        );
                    }
                }
                break;
            case Domain.Enums.BookingStatus.Forfeit:
                if (bookingToUpdate.PaymentPrice is not null)
                {
                    await _mediator.Send(
                        new CreateStripePaymentRequest
                        {
                            CustomerId = user.CustomerId ?? throw new CustomerNotFoundException(),
                            PaymentMethodId = user.PaymentMethodId ?? throw new PaymentMethodNotFoundException(),
                            Amount = bookingToUpdate.PaymentPrice ?? 0,
                            Currency = "EUR",
                            Description = "Pagamento Penale per mancata disdetta della prenotazione nei tempi prestabiliti per: " + activity.Name,
                            ReceiptEmail = "info@lacontessa.it",
                        },
                        cancellationToken
                    );
                }
                break;
            default:
                break;
        }

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
