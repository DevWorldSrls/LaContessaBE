namespace DevWorld.LaContessa.Stripe.Abstractions.Cards
{
    public class RetrieveStripeCard
    {
        public string CustomerId { get; set; } = null!;
        public string PaymentMethodId { get; set; } = null!;
    }
}
