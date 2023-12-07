using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetActivities : IRequest<GetActivities.Response>
{
    public class Response
    {
        public ActivityDetail[] Activities { get; set; }

        public class ActivityDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool IsOutdoor { get; set; }
            public string Description { get; set; }
            public string ActivityImg { get; set; }
            public List<Service> ServiceList { get; set; }
            public List<ActivityDate> DateList { get; set; }
        }

        public class Service
        {
            public string Icon { get; set; } // This should be the class name for an icon in a web application
            public string ServiceName { get; set; }
        }

        public class ActivityDate
        {
            public ActivityDate()
            {
                TimeSlotList = new List<ActivityTimeSlot>();
            }

            public string Date { get; set; } // Consider using DateTime for date representations
            public List<ActivityTimeSlot> TimeSlotList { get; set; }
        }

        public class ActivityTimeSlot
        {
            public string TimeSlot { get; set; } // Consider using TimeSpan or a custom struct
            public bool IsAlreadyBooked { get; set; }
        }
    }
}