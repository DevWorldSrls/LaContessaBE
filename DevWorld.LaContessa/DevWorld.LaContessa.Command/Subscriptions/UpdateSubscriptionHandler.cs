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
            throw new UserNotFoundException();

        subscriptionToUpdate.UserId = request.Subscription.UserId;
        subscriptionToUpdate.Number = request.Subscription.Number;
        subscriptionToUpdate.Valid = request.Subscription.Valid;

        await _laContessaDbContext.SaveChangesAsync();
    }
}
