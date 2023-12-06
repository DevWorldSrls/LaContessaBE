using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetSubscriptionByUserIdHandler : IRequestHandler<GetSubscriptionByUserId, GetSubscriptionByUserId.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionByUserIdHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscriptionByUserId.Response> Handle(GetSubscriptionByUserId request, CancellationToken cancellationToken)
    {
        return new GetSubscriptionByUserId.Response
        {
            Subscription = await _laContessaDbContext.Subscriptions
                .Where(x => x.UserId == request.UserId)
                .Select(x => new GetSubscriptionByUserId.Response.SubscriptionDetail
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CardNumber = x.CardNumber,
                    Valid = x.Valid,
                    ExpirationDate = x.ExpirationDate,
                    SubscriptionType = x.SubscriptionType
                })
                .FirstOrDefaultAsync(),
        };
    }
}