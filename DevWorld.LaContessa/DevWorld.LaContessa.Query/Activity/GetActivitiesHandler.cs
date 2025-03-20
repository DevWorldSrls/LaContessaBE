using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevWorld.LaContessa.Query.Activity;

public class GetActivitiesHandler : IRequestHandler<GetActivities, GetActivities.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetActivitiesHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetActivities.Response> Handle(GetActivities request, CancellationToken cancellationToken)
    {
        return new GetActivities.Response
        {
            Activities = await _laContessaDbContext.Activities
                .AsNoTracking()
                .AsQueryable()
                .Where(y => !y.IsDeleted)
                .Select(x => new GetActivities.Response.ActivityDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOutdoor = x.IsOutdoor,
                    IsSubscriptionRequired = x.IsSubscriptionRequired,
                    Description = x.Description,
                    ActivityImg = x.ActivityImg,
                    Price = x.Price,
                    Limit = x.Limit,
                    BookingType = x.BookingType,
                    Duration = x.Duration,
                    ExpirationDate = x.ExpirationDate,
                }).ToArrayAsync(cancellationToken)
        };
    }
}
