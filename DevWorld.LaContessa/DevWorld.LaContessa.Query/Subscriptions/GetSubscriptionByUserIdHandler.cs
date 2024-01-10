using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Subscriptions;

public class GetSubscriptionByUserIdHandler : IRequestHandler<GetSubscriptionByUserId, GetSubscriptionByUserId.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetSubscriptionByUserIdHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetSubscriptionByUserId.Response> Handle(GetSubscriptionByUserId request,
        CancellationToken cancellationToken)
    {
        return new GetSubscriptionByUserId.Response
        {
            Subscription = await _laContessaDbContext.Subscriptions
                .Select(x => new GetSubscriptionByUserId.Response.SubscriptionDetail
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
                })
                .Where(x => x.User.Id.ToString() == request.UserId)
                .ToArrayAsync(cancellationToken) ?? throw new SubscriptionNotFoundException()
        };
    }
}