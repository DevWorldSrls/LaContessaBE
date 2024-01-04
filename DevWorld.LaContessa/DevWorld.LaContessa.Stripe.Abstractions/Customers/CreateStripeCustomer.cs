using DevWorld.LaContessa.Stripe.Abstractions.Cards;
using MediatR;

namespace DevWorld.LaContessa.Stripe.Abstractions.Customers
{
    public class CreateStripeCustomer : IRequest
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public CreateStripeCard CreditCard { get; set; } = null!;
    }
}
