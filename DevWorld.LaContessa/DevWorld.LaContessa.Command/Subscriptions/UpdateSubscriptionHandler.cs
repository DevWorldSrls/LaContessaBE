﻿using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Stripe;
using DevWorld.LaContessa.Command.Abstractions.Subscriptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Subscriptions;

public class UpdateSubscriptionHandler : IRequestHandler<UpdateSbscription>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly IMediator _mediator;

    public UpdateSubscriptionHandler(
        LaContessaDbContext laContessaDbContext, 
        IMediator mediator
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _mediator = mediator;

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
        subscriptionToUpdate.SubscriptionPrice = request.Subscription.SubscriptionPrice;

        if (request.Subscription.IsPaymentRequest)
        {
            var result = await _mediator.Send(
                    new CreateStripePaymentRequest
                    {
                        CustomerId = user.CustomerId ?? throw new CustomerNotFoundException(),
                        PaymentMethodId = user.PaymentMethodId ?? throw new PaymentMethodNotFoundException(),
                        Amount = subscriptionToUpdate.SubscriptionPrice ?? 0,
                        Currency = "EUR",
                        Description = "Rinnovo abbonamento per: " + activity.Name,
                        ReceiptEmail = "info@lacontessa.it",
                    },
                    cancellationToken
                );

            if(result is null) throw new PaymentIntentNotFoundException();
        }

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
