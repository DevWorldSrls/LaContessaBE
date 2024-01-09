namespace DevWorld.LaContessa.Stripe.Abstractions.Customers
{
    public class CreateStripeCustomer
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
