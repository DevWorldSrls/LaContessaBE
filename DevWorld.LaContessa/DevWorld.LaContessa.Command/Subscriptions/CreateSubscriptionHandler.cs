using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Command.Abstractions.Utilities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscriptions;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateSubscription request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Subscriptions.AnyAsync(x => 
            request.Subscription.UserId == x.User.Id 
            && request.Subscription.ActivityId == x.Activity.Id
            , cancellationToken);

        if (alreadyExist)
            throw new SubscriptionAlreadyExistException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.Subscription.UserId, cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == request.Subscription.ActivityId, cancellationToken) ?? throw new ActivityNotFoundException();

        DateValidator.Validate(request.Subscription.ExpirationDate);
        DateValidator.Validate(request.Subscription.MedicalCertificateDueDate);

        var subscriptionToAdd = new Domain.Entities.Subscriptions.Subscription
        {
            Id = Guid.NewGuid(),
            User = user,
            Activity = activity,
            CardNumber = request.Subscription.CardNumber,
            IsValid = request.Subscription.IsValid,
            ExpirationDate = request.Subscription.ExpirationDate,
            SubType = request.Subscription.SubType,
            NumberOfIngress = request.Subscription.NumberOfIngress,
            MedicalCertificateExpired = request.Subscription.MedicalCertificateExpired,
            MedicalCertificateDueDate = request.Subscription.MedicalCertificateDueDate,
            SubscriptionPrice = request.Subscription.SubscriptionPrice,
        };

        await _laContessaDbContext.AddAsync(subscriptionToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
