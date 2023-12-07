using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptions, GetSubscriptions.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionsHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscriptions.Response> Handle(GetSubscriptions request, CancellationToken cancellationToken)
    {
        return new GetSubscriptions.Response
        {
            Subscriptions = await _laContessaDbContext.Subscriptions
                .AsQueryable()
                .Select(x => new GetSubscriptions.Response.SubscriptionDetail
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CardNumber = x.CardNumber,
                    Valid = x.Valid,
                    ExpirationDate = x.ExpirationDate,
                    SubscriptionType = x.SubscriptionType
                }).ToArrayAsync()
        };
    }
}
