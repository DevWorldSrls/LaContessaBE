using DevWorld.LaContessa.Command.Abstractions.Booking;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Booking;

public class UpdateActivityHandler : IRequestHandler<UpdateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateActivityHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateActivity request, CancellationToken cancellationToken)
    {
        var activityToUpdate = await _laContessaDbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.Activity.Id && !x.IsDeleted, cancellationToken) ?? throw new ActivityNotFoundException();

        activityToUpdate.Name = request.Activity.Name;
        activityToUpdate.Name = request.Activity.Name;
        activityToUpdate.IsOutdoor = request.Activity.IsOutdoor;
        activityToUpdate.Description = request.Activity.Description;
        activityToUpdate.ActivityImg = request.Activity.ActivityImg;
        activityToUpdate.ServiceList = request.Activity.ServiceList.Select(service =>
            new Service
            {
                Icon = service.Icon,
                ServiceName = service.ServiceName
            }).ToList();

        activityToUpdate.DateList = request.Activity.DateList.Select(date =>
            new ActivityDate
            {
                Date = date.Date,
                TimeSlotList = date.TimeSlotList.Select(ts =>
                    new ActivityTimeSlot
                    {
                        TimeSlot = ts.TimeSlot,
                        IsAlreadyBooked = ts.IsAlreadyBooked
                    }).ToList()
            }).ToList();

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
