using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
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

    public RefundRequestHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
    }

    public async Task Handle(RefundRequest request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _laContessaDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == request.BookingId && !x.IsDeleted, cancellationToken) ?? throw new BookingNotFoundException();

        if (!CanPerformRefund(bookingToUpdate.Date, bookingToUpdate.TimeSlot)) throw new RefundNotAvailableException();

        await _stripeAppService.RefundStripePaymentAsync(
            bookingToUpdate.PaymentIntentId ?? throw new PaymentIntentNotFoundException(),
            cancellationToken);

        bookingToUpdate.PaymentIntentId = null;
        bookingToUpdate.Status = Domain.Enums.BookingStatus.Cancelled;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
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
