using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Activities;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Activity;

public class GetActivityHandler : IRequestHandler<GetActivity, GetActivity.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetActivity.Response> Handle(GetActivity request, CancellationToken cancellationToken)
    {
        return new GetActivity.Response
        {
            Activity = await _laContessaDbContext.Activities
                .Select(x => new GetActivity.Response.ActivityDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsOutdoor = x.IsOutdoor,
                    IsSubscriptionRequired = x.IsSubscriptionRequired,
                    Description = x.Description,
                    ActivityImg = x.ActivityImg,
                    Price = x.Price,
                    ServiceList = x.ServiceList.Select(service => new GetActivity.Response.Service
                    {
                        Icon = service.Icon,
                        ServiceName = service.ServiceName
                    }).ToList(),
                    DateList = x.DateList.Select(date => new GetActivity.Response.ActivityDate
                    {
                        Date = date.Date,
                        TimeSlotList = date.TimeSlotList.Select(ts => new GetActivity.Response.ActivityTimeSlot
                        {
                            TimeSlot = ts.TimeSlot,
                            IsAlreadyBooked = ts.IsAlreadyBooked
                        }).ToList()
                    }).ToList(),
                    Limit = x.Limit,
                    BookingType = x.BookingType,
                    Duration = x.Duration
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new ActivityNotFoundException()
        };
    }
}