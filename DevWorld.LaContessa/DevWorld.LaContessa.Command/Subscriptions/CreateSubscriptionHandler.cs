using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscription;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateSubscription request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Subscriptions.Where(x => request.Subscription.UserId == x.UserId).AnyAsync();

        if (alreadyExist)
            throw new SubscriptionAlreadyExistException();

        var subscriptionToAdd = new Domain.Entities.Subscriptions.Subscription
        { 
            Id = Guid.NewGuid(),
            UserId = request.Subscription.UserId,
            CardNumber = request.Subscription.CardNumber,
            Valid = request.Subscription.Valid,
            ExpirationDate = request.Subscription.ExpirationDate,
            SubscriptionType = request.Subscription.SubscriptionType
        };

        await _laContessaDbContext.AddAsync(subscriptionToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
