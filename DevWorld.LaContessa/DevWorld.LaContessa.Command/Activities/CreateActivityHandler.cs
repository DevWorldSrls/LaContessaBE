using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Activities;

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

        var activityToAdd = new Activity
        {
            Id = Guid.NewGuid(),
            Name = request.Activity.Name,
            IsOutdoor = request.Activity.IsOutdoor,
            IsSubscriptionRequired = request.Activity.IsSubscriptionRequired,
            Description = request.Activity.Description,
            ActivityImg = request.Activity.ActivityImg,
            Price = request.Activity.Price,
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
            }).ToList(),
            Limit = request.Activity.Limit,
            BookingType = request.Activity.BookingType,
            Duration = request.Activity.Duration
        };

        await _laContessaDbContext.AddAsync(activityToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
