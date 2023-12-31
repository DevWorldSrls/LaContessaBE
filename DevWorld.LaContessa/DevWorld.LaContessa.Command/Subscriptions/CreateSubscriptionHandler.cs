﻿using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
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
        var alreadyExist = await _laContessaDbContext.Subscriptions.AnyAsync(x => request.Subscription.UserId == x.User.Id.ToString(), cancellationToken);

        if (alreadyExist)
            throw new SubscriptionAlreadyExistException();

        var user = await _laContessaDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(request.Subscription.UserId), cancellationToken) ?? throw new UserNotFoundException();

        var activity = await _laContessaDbContext.Activities
            .FirstOrDefaultAsync(a => a.Id == Guid.Parse(request.Subscription.ActivityId), cancellationToken) ?? throw new ActivityNotFoundException();

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
        };

        await _laContessaDbContext.AddAsync(subscriptionToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
