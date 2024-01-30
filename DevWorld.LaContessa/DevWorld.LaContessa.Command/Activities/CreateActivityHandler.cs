using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Activities;

public class CreateActivityHandler : IRequestHandler<CreateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public CreateActivityHandler(LaContessaDbContext laContessaDbContext, ILaContessaFirebaseStorage laContessaFirebaseStorage)
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(CreateActivity request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Activities.AnyAsync(x => request.Activity.Name == x.Name, cancellationToken);

        if (alreadyExist)
            throw new ActivityAlreadyExistException();

        string? imageUrl = null;
        var newActivityId = Guid.NewGuid();

        if (!(string.IsNullOrEmpty(request.Activity.ActivityImg) || string.IsNullOrEmpty(request.Activity.ActivityImgExt)))
        {
            imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.Activity.ActivityImg, "activities", newActivityId + request.Activity.ActivityImgExt);
        }

        var activityToAdd = new Activity
        {
            Id = newActivityId,
            Name = request.Activity.Name,
            IsOutdoor = request.Activity.IsOutdoor,
            IsSubscriptionRequired = request.Activity.IsSubscriptionRequired,
            Description = request.Activity.Description,
            ActivityImg = imageUrl,
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
            Duration = request.Activity.Duration,
            ExpirationDate = request.Activity.ExpirationDate,
            ActivityVariants = request.Activity.ActivityVariants is null 
                ? null 
                : request.Activity.ActivityVariants.Select(v => new ActivityVariants
                {
                    Variant = v.Variant,
                    Price = v.Price
                }).ToList(),
        };

        await _laContessaDbContext.AddAsync(activityToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
