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
        var alreadyExist = await _laContessaDbContext.Subscriptions.AnyAsync(x => request.Subscription.UserId == x.User.Id.ToString(), cancellationToken);

        if (alreadyExist)
            throw new SubscriptionAlreadyExistException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(request.Subscription.UserId), cancellationToken) ?? throw new UserNotFoundException();
        var subscriptionToAdd = new Domain.Entities.Subscriptions.Subscription
        {
            Id = Guid.NewGuid(),
            User = user,
            CardNumber = request.Subscription.CardNumber,
            Valid = request.Subscription.Valid,
            ExpirationDate = request.Subscription.ExpirationDate,
            SubscriptionType = request.Subscription.SubscriptionType
        };

        await _laContessaDbContext.AddAsync(subscriptionToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
