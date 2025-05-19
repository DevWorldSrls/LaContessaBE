using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Bookings;
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
                .Where(y => !y.IsDeleted)
                .Select(x => new GetSubscriptionByUserId.Response.SubscriptionDetail
                {
                    Id = x.Id,
                    User = x.User,
                    Activity = new GetSubscriptionByUserId.Response.SubscriptionDetail.ActivityDetail
                    {
                        Id = x.Activity.Id,
                        Name = x.Activity.Name,
                        IsOutdoor = x.Activity.IsOutdoor,
                        IsSubscriptionRequired = x.Activity.IsSubscriptionRequired,
                        Description = x.Activity.Description,
                        ActivityImg = x.Activity.ActivityImg,
                        Price = x.Activity.Price,
                        Limit = x.Activity.Limit,
                        BookingType = x.Activity.BookingType,
                        Duration = x.Activity.Duration,
                        ExpirationDate = x.Activity.ExpirationDate,
                    },
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
                .Where(x => x.User.Id.ToString() == request.UserId)
                .ToArrayAsync(cancellationToken) ?? throw new SubscriptionNotFoundException()
        };
    }
}