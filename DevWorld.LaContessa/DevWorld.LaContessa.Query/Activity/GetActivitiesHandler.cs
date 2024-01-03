using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                .AsQueryable()
                .Select(x => new GetActivities.Response.ActivityDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOutdoor = x.IsOutdoor,
                    IsSubscriptionRequired = x.IsSubscriptionRequired,
                    Description = x.Description,
                    ActivityImg = x.ActivityImg,
                    Price = x.Price,
                    ServiceList = x.ServiceList.Select(service => new GetActivities.Response.Service
                    {
                        Icon = service.Icon,
                        ServiceName = service.ServiceName
                    }).ToList(),
                    DateList = x.DateList.Select(date => new GetActivities.Response.ActivityDate
                    {
                        Date = date.Date,
                        TimeSlotList = date.TimeSlotList.Select(ts => new GetActivities.Response.ActivityTimeSlot
                        {
                            TimeSlot = ts.TimeSlot,
                            IsAlreadyBooked = ts.IsAlreadyBooked
                        }).ToList()
                    }).ToList(),
                    Limit = x.Limit,
                    BookingType = x.BookingType
                }).ToArrayAsync(cancellationToken)
        };
    }
}
