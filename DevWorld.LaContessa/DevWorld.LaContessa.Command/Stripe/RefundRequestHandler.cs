using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        await _stripeAppService.RefundStripePaymentAsync(
            bookingToUpdate.PaymentIntentId ?? throw new PaymentIntentNotFoundException(),
            cancellationToken);

        bookingToUpdate.PaymentIntentId = null;
        bookingToUpdate.Status = Domain.Enums.BookingStatus.Cancelled;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
