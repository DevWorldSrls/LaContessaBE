using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscriptions;

public class UpdateSubscriptionHandler : IRequestHandler<UpdateSbscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateSbscription request, CancellationToken cancellationToken)
    {
        var subscriptionToUpdate = await _laContessaDbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.Subscription.Id && !x.IsDeleted, cancellationToken) ?? throw new SubscriptionNotFoundException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.Subscription.UserId, cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == request.Subscription.ActivityId, cancellationToken) ?? throw new ActivityNotFoundException();

        subscriptionToUpdate.User = user;
        subscriptionToUpdate.Activity = activity;
        subscriptionToUpdate.CardNumber = request.Subscription.CardNumber;
        subscriptionToUpdate.IsValid = request.Subscription.IsValid;
        subscriptionToUpdate.ExpirationDate = request.Subscription.ExpirationDate;
        subscriptionToUpdate.SubType = request.Subscription.SubType;
        subscriptionToUpdate.NumberOfIngress = request.Subscription.NumberOfIngress;
        subscriptionToUpdate.MedicalCertificateExpired = request.Subscription.MedicalCertificateExpired;
        subscriptionToUpdate.MedicalCertificateDueDate = request.Subscription.MedicalCertificateDueDate;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
