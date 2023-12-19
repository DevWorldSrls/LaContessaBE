using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetSubscriptionHandler : IRequestHandler<GetSubscription, GetSubscription.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscription.Response> Handle(GetSubscription request, CancellationToken cancellationToken)
    {
        return new GetSubscription.Response
        {
            Subscription = await _laContessaDbContext.Subscriptions
                .Select(x => new GetSubscription.Response.SubscriptionDetail
                {
                    Id = x.Id,
                    User = x.User,
                    Activity = x.Activity,
                    CardNumber = x.CardNumber,
                    Valid = x.Valid,
                    ExpirationDate = x.ExpirationDate,
                    SubscriptionType = x.SubscriptionType
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
        };
    }
}