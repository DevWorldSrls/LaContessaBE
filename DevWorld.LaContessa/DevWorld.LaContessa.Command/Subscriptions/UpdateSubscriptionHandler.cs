using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscription;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscription;

public class UpdateSubscriptionHandler : IRequestHandler<UpdateSbscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateSbscription request, CancellationToken cancellationToken)
    {
        var subscriptionToUpdate = await _laContessaDbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.Subscription.Id && !x.IsDeleted);

        if (subscriptionToUpdate is null)
            throw new SubscriptionNotFoundException();

        subscriptionToUpdate.UserId = request.Subscription.UserId;
        subscriptionToUpdate.UserId = request.Subscription.UserId;
        subscriptionToUpdate.CardNumber = request.Subscription.CardNumber;
        subscriptionToUpdate.Valid = request.Subscription.Valid;
        subscriptionToUpdate.ExpirationDate = request.Subscription.ExpirationDate;
        subscriptionToUpdate.SubscriptionType = request.Subscription.SubscriptionType;

        await _laContessaDbContext.SaveChangesAsync();
    }
}
