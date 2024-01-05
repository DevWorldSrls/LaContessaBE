using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Stripe;

public class CreateStripeCustomerRequestHandler : IRequestHandler<CreateStripeCustomerRequest>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IStripeAppService _stripeAppService;

    public CreateStripeCustomerRequestHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
    }

    public async Task Handle(CreateStripeCustomerRequest request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Customer.UserId && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        var result = await _stripeAppService.CreateStripeCustomerAsync(
            new LaContessa.Stripe.Abstractions.Customers.CreateStripeCustomer
            {
                Email = request.Customer.Email,
                Name = request.Customer.Name,
                CreditCard = new LaContessa.Stripe.Abstractions.Cards.CreateStripeCard
                {
                    Name = request.Customer.CreditCard.Name,
                    CardNumber = request.Customer.CreditCard.CardNumber,
                    Cvc = request.Customer.CreditCard.Cvc,
                    ExpirationMonth = request.Customer.CreditCard.ExpirationMonth,
                    ExpirationYear = request.Customer.CreditCard.ExpirationYear
                }
            },
            cancellationToken);


        userToUpdate.CustomerId = result.CustomerId;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
