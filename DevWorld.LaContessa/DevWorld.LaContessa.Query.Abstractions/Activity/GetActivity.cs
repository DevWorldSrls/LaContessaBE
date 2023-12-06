using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetActivity : IRequest<GetActivity.Response>
{
    public Guid Id { get; set; }

    public GetActivity(Guid id)
    {
        Id = id;
    }

    public class Response
    {
        public ActivityDetail? Activity { get; set; }

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
            public string Date { get; set; } // Consider using DateTime for date representations
            public List<ActivityTimeSlot> TimeSlotList { get; set; }

            public ActivityDate()
            {
                TimeSlotList = new List<ActivityTimeSlot>();
            }
        }

        public class ActivityTimeSlot
        {
            public string TimeSlot { get; set; } 
            public bool IsAlreadyBooked { get; set; }
        }
    }

}
