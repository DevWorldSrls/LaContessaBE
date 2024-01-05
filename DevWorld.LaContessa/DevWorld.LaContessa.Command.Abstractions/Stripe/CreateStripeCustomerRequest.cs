using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Stripe
{
    public class CreateStripeCustomerRequest : IRequest
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
            public string ExpirationYear { get; set; } = null!;
            public string ExpirationMonth { get; set; } = null!;
            public string Cvc { get; set; } = null!;
        }
    }
}
