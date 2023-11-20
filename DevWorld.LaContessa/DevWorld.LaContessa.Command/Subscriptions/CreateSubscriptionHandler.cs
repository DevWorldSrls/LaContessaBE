using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Persistance;
//using DevWorld.LaContessa.Domain.Entities.Subscriptions.Subscription;
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
            Number = request.Subscription.Number,
            Valid = request.Subscription.Valid
        };

        await _laContessaDbContext.AddAsync(subscriptionToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
