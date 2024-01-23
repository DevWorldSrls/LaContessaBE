using DevWorld.LaContessa.Stripe.Abstractions.Cards;
using DevWorld.LaContessa.Stripe.Abstractions.Customers;
using DevWorld.LaContessa.Stripe.Abstractions.Payments;

namespace DevWorld.LaContessa.Stripe
{
    public interface IStripeAppService
    {
        Task<StripeCustomer> AddStripeCustomerCard(CreateStripeCard card, string? customerId = null, CreateStripeCustomer? customer = null, CancellationToken ct = default);
        Task<StripePayment> CreateStripePaymentAsync(CreateStripePayment payment, CancellationToken ct);
        Task<RetrieveStripeCardResponse> RetrieveStripeCustomerCard(RetrieveStripeCardRequest retrieveRequest, CancellationToken ct);
        Task DeleteStripePaymentAsync(string paymentId, CancellationToken ct);
        Task RefundStripePaymentAsync(string paymentIntentId, CancellationToken ct);
    }
}
