﻿using DevWorld.LaContessa.Command.Abstractions.Activites;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Utilities;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace DevWorld.LaContessa.Command.Activities;

public class UpdateActivityHandler : IRequestHandler<UpdateActivity>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public UpdateActivityHandler(LaContessaDbContext laContessaDbContext, ILaContessaFirebaseStorage laContessaFirebaseStorage)
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(UpdateActivity request, CancellationToken cancellationToken)
    {
        var activityToUpdate = await _laContessaDbContext.Activities.FirstOrDefaultAsync(x => x.Id == request.Activity.Id && !x.IsDeleted, cancellationToken) ?? throw new ActivityNotFoundException();

        string? imageUrl = null;

        if (!string.IsNullOrEmpty(request.Activity.ActivityImg))
        {
            if (activityToUpdate.ActivityImg != request.Activity.ActivityImg && !(string.IsNullOrEmpty(request.Activity.ActivityImgExt)))
            {
                imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.Activity.ActivityImg, "activities", activityToUpdate.Id + request.Activity.ActivityImgExt);
            } 
            else
            {
                imageUrl = request.Activity.ActivityImg;
            }
        }

        foreach (var date in request.Activity.DateList)
        {
            DateValidator.Validate(date.Date);
        }

        DateValidator.Validate(request.Activity.ExpirationDate);

        activityToUpdate.Name = request.Activity.Name;
        activityToUpdate.Name = request.Activity.Name;
        activityToUpdate.IsOutdoor = request.Activity.IsOutdoor;
        activityToUpdate.IsSubscriptionRequired = request.Activity.IsSubscriptionRequired;
        activityToUpdate.Description = request.Activity.Description;
        activityToUpdate.ActivityImg = imageUrl;
        activityToUpdate.Price = request.Activity.Price;
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
        activityToUpdate.Limit = request.Activity.Limit;
        activityToUpdate.BookingType = request.Activity.BookingType;
        activityToUpdate.Duration = request.Activity.Duration;
        activityToUpdate.ExpirationDate = request.Activity.ExpirationDate;
        
        if(request.Activity.ActivityVariants is not null)
        {
            activityToUpdate.ActivityVariants = request.Activity.ActivityVariants.Select(v =>
                new ActivityVariant
                {
                    Variant = v.Variant,
                    Price = v.Price
                }).ToList();
        }

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
