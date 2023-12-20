using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Activites;

public class CreateActivity : IRequest
{
    public ActivityDetail Activity { get; set; } = null!;

    public class ActivityDetail
    {
        public string Name { get; set; } = null!;
        public bool IsOutdoor { get; set; }
        public bool IsSubscriptionRequired { get; set; }
        public string Description { get; set; } = null!;
        public string ActivityImg { get; set; } = null!;
        public List<Service> ServiceList { get; set; } = null!;
        public List<ActivityDate> DateList { get; set; } = null!;
    }

    public class Service
    {
        public string Icon { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
    }

    public class ActivityDate
    {
        public string Date { get; set; } = null!;
        public List<ActivityTimeSlot> TimeSlotList { get; set; } = null!;
    }

    public class ActivityTimeSlot
    {
        public string TimeSlot { get; set; } = null!;
        public bool IsAlreadyBooked { get; set; }
    }
}
