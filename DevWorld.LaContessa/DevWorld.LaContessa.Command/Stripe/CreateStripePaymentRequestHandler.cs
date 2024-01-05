using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using MediatR;

namespace DevWorld.LaContessa.Command.Stripe;

public class CreateStripePaymentRequestHandler : IRequestHandler<CreateStripePaymentRequest>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IStripeAppService _stripeAppService;

    public CreateStripePaymentRequestHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
    }

    public async Task Handle(CreateStripePaymentRequest request, CancellationToken cancellationToken)
    {
        var result = await _stripeAppService.CreateStripePaymentAsync(
            new LaContessa.Stripe.Abstractions.Payments.CreateStripePayment
            {
                Amount = request.Amount,
                Currency = request.Currency,
                CustomerId  = request.CustomerId,
                Description = request.Description,
                ReceiptEmail = request.ReceiptEmail,
            },
            cancellationToken);

        return;
    }
}
