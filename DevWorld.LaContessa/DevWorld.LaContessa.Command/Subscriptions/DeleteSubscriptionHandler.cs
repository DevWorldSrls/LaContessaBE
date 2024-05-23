using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscriptions;

public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public DeleteSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(DeleteSubscription request, CancellationToken cancellationToken)
    {
        var subscriptionToUpdate = await _laContessaDbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken) ?? throw new SubscriptionNotFoundException();

        _laContessaDbContext.Subscriptions.Remove(subscriptionToUpdate);

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}