namespace DevWorld.LaContessa.Stripe.Abstractions.Cards
{
    public class RetrieveStripeCardResponse
    {
        public string CardHolder { get; set; } = null!;
        public string LastFour { get; set; } = null!;
        public long ExpirationYear { get; set; }
        public long ExpirationMonth { get; set; }
        public string Brand { get; set; } = null!;
    }
}
