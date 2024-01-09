using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Stripe;
using DevWorld.LaContessa.Stripe.Abstractions.Payments;
using MediatR;

namespace DevWorld.LaContessa.Command.Stripe;

public class CreateStripePaymentRequestHandler : IRequestHandler<CreateStripePaymentRequest, string>
{
    private readonly IStripeAppService _stripeAppService;

    public CreateStripePaymentRequestHandler(
        IStripeAppService stripeAppService
    )
    {
        _stripeAppService = stripeAppService;
    }

    public async Task<string> Handle(CreateStripePaymentRequest request, CancellationToken cancellationToken)
    {
        var result = await _stripeAppService.CreateStripePaymentAsync(
            new CreateStripePayment
            {
                Amount = request.Amount,
                Currency = request.Currency,
                CustomerId  = request.CustomerId,
                Description = request.Description,
                ReceiptEmail = request.ReceiptEmail,
                PaymentMethodId = request.PaymentMethodId,
            },
            cancellationToken);

        return result.PaymentId;
    }
}
