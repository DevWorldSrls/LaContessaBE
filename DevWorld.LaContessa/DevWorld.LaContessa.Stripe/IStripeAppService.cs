using DevWorld.LaContessa.Stripe.Abstractions.Customers;
using DevWorld.LaContessa.Stripe.Abstractions.Payments;

namespace DevWorld.LaContessa.Stripe
{
    public interface IStripeAppService
    {
        Task<StripeCustomer> CreateStripeCustomerAsync(CreateStripeCustomer customer, CancellationToken ct);
        Task<StripePayment> CreateStripePaymentAsync(CreateStripePayment payment, CancellationToken ct);
    }
}
