using DevWorld.LaContessa.Command.Abstractions.Bookings;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Domain.Enums;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DevWorld.LaContessa.Command.Stripe;

public class RefundRequestHandler : IRequestHandler<RefundRequest>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IStripeAppService _stripeAppService;
    private readonly IMediator _mediator;

    public RefundRequestHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService,
        IMediator mediator
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
        _mediator = mediator;
    }

    public async Task Handle(RefundRequest request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _laContessaDbContext.Bookings
            .Include( x => x.User)
            .Include(x => x.Activity)
            .FirstOrDefaultAsync(x => x.Id == request.BookingId && !x.IsDeleted, cancellationToken) ?? throw new BookingNotFoundException();

        if (!CanPerformRefund(bookingToUpdate.Date, bookingToUpdate.TimeSlot)) throw new RefundNotAvailableException();

        await _stripeAppService.RefundStripePaymentAsync(
            bookingToUpdate.PaymentIntentId ?? throw new PaymentIntentNotFoundException(),
            cancellationToken);

        await _mediator.Send(
            new UpdateBooking
            {
                Booking = new UpdateBooking.BookingDetail
                {
                    Id = bookingToUpdate.Id,
                    UserId = bookingToUpdate.User.Id,
                    ActivityId = bookingToUpdate.Activity.Id,
                    Date = bookingToUpdate.Date,
                    TimeSlot = bookingToUpdate.TimeSlot,
                    BookingName = bookingToUpdate.BookingName,
                    PhoneNumber = bookingToUpdate.PhoneNumber,
                    IsLesson = bookingToUpdate.IsLesson,
                    Status = BookingStatus.Cancelled,
                    BookingPrice = bookingToUpdate.BookingPrice,
                    PaymentPrice = bookingToUpdate?.PaymentPrice,
                }
            },
            cancellationToken
        );
    }

    private bool CanPerformRefund(string bookingDate, string bookingTime)
    {
        var bookingDateParsed = DateTime.ParseExact(bookingDate, "dd MMM yyyy", CultureInfo.CreateSpecificCulture("it-IT"));

        var time = bookingTime.Split(':');
        if (time is null) return false;

        var hours = double.Parse(time[0]);
        var minutes = double.Parse(time[1]);

        var dateWithHour = bookingDateParsed.AddHours(hours);
        var dateCompleted = dateWithHour.AddMinutes(minutes);
        
        return (dateCompleted - DateTime.Now).TotalHours > 25;
    }
}
