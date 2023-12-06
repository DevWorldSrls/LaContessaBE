using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Domain.Entities.Activities;

public class Activity : SoftDeletable
{
    public string Name { get; set; }
    public bool IsOutdoor { get; set; }
    public string Description { get; set; } 
    public string ActivityImg { get; set; } 
    public List<Service> ServiceList { get; set; }
    public List<ActivityDate> DateList { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Activity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

[Owned]
public class Service
{
    public string Icon { get; set; } // This should be the class name for an icon in a web application
    public string ServiceName { get; set; }
}

[Owned]
public class ActivityDate
{
    public string Date { get; set; } // Consider using DateTime for date representations
    public List<ActivityTimeSlot> TimeSlotList { get; set; }

    public ActivityDate()
    {
        TimeSlotList = new List<ActivityTimeSlot>();
    }
}

[Owned]
public class ActivityTimeSlot
{
    public string TimeSlot { get; set; } // Consider using TimeSpan or a custom struct
    public bool IsAlreadyBooked { get; set; }
}


