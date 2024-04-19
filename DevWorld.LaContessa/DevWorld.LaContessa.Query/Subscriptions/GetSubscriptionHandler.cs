using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Subscriptions;

public class GetSubscriptionHandler : IRequestHandler<GetSubscription, GetSubscription.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscription.Response> Handle(GetSubscription request, CancellationToken cancellationToken)
    {
        return new GetSubscription.Response
        {
            Subscription = await _laContessaDbContext.Subscriptions
                .Select(x => new GetSubscription.Response.SubscriptionDetail
                {
                    Id = x.Id,
                    User = x.User,
                    Activity = x.Activity,
                    CardNumber = x.CardNumber,
                    IsValid = x.IsValid,
                    ExpirationDate = x.ExpirationDate,
                    SubType = x.SubType,
                    NumberOfIngress = x.NumberOfIngress,
                    MedicalCertificateExpired = x.MedicalCertificateExpired,
                    MedicalCertificateDueDate = x.MedicalCertificateDueDate,
                    SubscriptionPrice = x.SubscriptionPrice,
                    InitialPrice = x.InitialPrice,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new SubscriptionNotFoundException()
        };
    }
}