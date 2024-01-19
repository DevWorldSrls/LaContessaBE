namespace DevWorld.LaContessa.Stripe.Abstractions.Cards
{
    public class CreateStripeCard
    {
        public string Name { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public long ExpirationYear { get; set; }
        public long ExpirationMonth { get; set; }
        public string Cvc { get; set; } = null!;
    }
}
