using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Activities;

public class GetActivity : IRequest<GetActivity.Response>
{
    public GetActivity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    public class Response
    {
        public ActivityDetail? Activity { get; set; } = null!;

        public class ActivityDetail
        {
            public Guid Id { get; set; }
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
            public List<string>? ActivityVariants { get; set; }
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
