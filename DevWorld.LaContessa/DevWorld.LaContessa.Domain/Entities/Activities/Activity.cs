using DevWorld.LaContessa.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Domain.Entities.Activities;

public class Activity : SoftDeletable
{
    public string Name { get; set; } = null!;
    public bool IsOutdoor { get; set; }
    public bool IsSubscriptionRequired { get; set; }
    public double Price { get; set; }
    public List<Service> ServiceList { get; set; } = null!;
    public List<ActivityDate> DateList { get; set; } = null!;
    public ActivityBookingType BookingType { get; set; }
    public string Duration { get; set; } = null!;
    public int? Limit { get; set; }
    public string? Description { get; set; }
    public string? ActivityImg { get; set; }
    public string? ExpirationDate { get; set; }
}

[Owned]
public class Service
{
    public string Icon { get; set; } = null!; 
    public string ServiceName { get; set; } = null!;
}

[Owned]
public class ActivityDate
{
    public string Date { get; set; } = null!; 
    public List<ActivityTimeSlot> TimeSlotList { get; set; } = null!;
}

[Owned]
public class ActivityTimeSlot
{
    public string TimeSlot { get; set; } = null!; 
    public bool IsAlreadyBooked { get; set; }
}


