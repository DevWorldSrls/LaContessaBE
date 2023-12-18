using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetActivities : IRequest<GetActivities.Response>
{
    public class Response
    {
        public ActivityDetail[] Activities { get; set; } = null!;

        public class ActivityDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public bool IsOutdoor { get; set; }
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
}