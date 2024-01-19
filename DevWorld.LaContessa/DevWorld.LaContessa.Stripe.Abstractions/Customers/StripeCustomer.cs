namespace DevWorld.LaContessa.Stripe.Abstractions.Customers
{
    public class StripeCustomer
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string PaymentMethodId { get; set; } = null!;
    }
}
