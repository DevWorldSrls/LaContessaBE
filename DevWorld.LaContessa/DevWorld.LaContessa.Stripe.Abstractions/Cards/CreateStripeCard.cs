using MediatR;

namespace DevWorld.LaContessa.Stripe.Abstractions.Cards
{
    public class CreateStripeCard
    {
        public string Name { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string ExpirationYear { get; set; } = null!;
        public string ExpirationMonth { get; set; } = null!;
        public string Cvc { get; set; } = null!;
    }
}
