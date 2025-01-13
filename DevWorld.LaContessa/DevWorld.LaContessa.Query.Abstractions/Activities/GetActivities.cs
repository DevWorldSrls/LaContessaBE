using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Activities;

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
            public bool IsSubscriptionRequired { get; set; }
            public double? Price { get; set; }
            public ActivityBookingType BookingType { get; set; }
            public string Duration { get; set; } = null!;
            public int? Limit { get; set; }
            public string? Description { get; set; }
            public string? ActivityImg { get; set; }
            public string? ExpirationDate { get; set; }
        }
    }
}