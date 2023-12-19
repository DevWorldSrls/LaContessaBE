using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Activity;

public class CreateActivityHandler : IRequestHandler<CreateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateActivity request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Activities.AnyAsync(x => request.Activity.Name == x.Name, cancellationToken);

        if (alreadyExist)
            throw new ActivityAlreadyExistException();

        var activityToAdd = new Domain.Entities.Activities.Activity
        {
            Id = Guid.NewGuid(),
            Name = request.Activity.Name,
            IsOutdoor = request.Activity.IsOutdoor,
            IsSubscriptionRequired = request.Activity.IsSubscriptionRequired,
            Description = request.Activity.Description,
            ActivityImg = request.Activity.ActivityImg,
            ServiceList = request.Activity.ServiceList.Select(service => new Service
            {
                Icon = service.Icon,
                ServiceName = service.ServiceName
            }).ToList(),
            DateList = request.Activity.DateList.Select(date => new ActivityDate
            {
                Date = date.Date,
                TimeSlotList = date.TimeSlotList.Select(ts => new ActivityTimeSlot
                {
                    TimeSlot = ts.TimeSlot,
                    IsAlreadyBooked = ts.IsAlreadyBooked
                }).ToList()
            }).ToList()
        };

        await _laContessaDbContext.AddAsync(activityToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
