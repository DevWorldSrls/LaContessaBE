using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Stripe
{
    public class CreateCard : IRequest
    {
        public StripeCustomer Customer { get; set; } = null!;

        public class StripeCustomer
        {
            public Guid UserId { get; set; }
            public string Email { get; set; } = null!;
            public string Name { get; set; } = null!;
            public StripeCard CreditCard { get; set; } = null!;
        }

        public class StripeCard
        {
            public string Name { get; set; } = null!;
            public string CardNumber { get; set; } = null!;
            public long ExpirationYear { get; set; }
            public long ExpirationMonth { get; set; }
            public string Cvc { get; set; } = null!;
        }
    }
}
