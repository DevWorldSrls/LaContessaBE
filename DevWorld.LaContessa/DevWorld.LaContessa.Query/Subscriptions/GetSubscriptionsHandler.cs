using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Subscriptions;

public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptions, GetSubscriptions.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionsHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscriptions.Response> Handle(GetSubscriptions request, CancellationToken cancellationToken)
    {
        return new GetSubscriptions.Response
        {
            Subscriptions = await _laContessaDbContext.Subscriptions
                .AsQueryable()
                .Select(x => new GetSubscriptions.Response.SubscriptionDetail
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
                }).ToArrayAsync(cancellationToken)
        };
    }
}
