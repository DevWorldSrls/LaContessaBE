using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Stripe;
using DevWorld.LaContessa.Stripe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DevWorld.LaContessa.Query.Abstractions.Stripe.GetCard.Response;

namespace DevWorld.LaContessa.Query.Stripe;

public class GetCardHandler : IRequestHandler<GetCard, GetCard.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IStripeAppService _stripeAppService;

    public GetCardHandler(
        LaContessaDbContext laContessaDbContext,
        IStripeAppService stripeAppService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _stripeAppService = stripeAppService;
    }

    public async Task<GetCard.Response> Handle(GetCard request, CancellationToken cancellationToken)
    {
        var user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId) ?? throw new UserNotFoundException();

        if (user.CustomerId is null || user.PaymentMethodId is null) throw new PaymentMethodNotFoundException();

        var stripeCard = await _stripeAppService.RetrieveStripeCustomerCard(
            new LaContessa.Stripe.Abstractions.Cards.RetrieveStripeCard
            {
                CustomerId = user.CustomerId,
                PaymentMethodId = user.PaymentMethodId,
            },
            cancellationToken
        );

        if(stripeCard.CardNumber is null) throw new PaymentMethodNotFoundException();

        return new GetCard.Response
        {
            Card = new CardDetail
            {
                LastFour = stripeCard.CardNumber,
                ExpirationMonth = stripeCard.ExpirationMonth,
                ExpirationYear = stripeCard.ExpirationYear,
            }
        };
    }
}
