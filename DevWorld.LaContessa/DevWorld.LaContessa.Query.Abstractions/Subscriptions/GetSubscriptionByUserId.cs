using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Subscriptions;

public class GetSubscriptionByUserId : IRequest<GetSubscriptionByUserId.Response>
{
    public GetSubscriptionByUserId(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; } = null!;

    public class Response
    {
        public SubscriptionDetail[]? Subscription { get; set; }

        public class SubscriptionDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; } = null!;
            public ActivityDetail Activity { get; set; } = null!;
            public SubscriptionType SubType { get; set; }
            public bool IsValid { get; set; } = true;
            public bool MedicalCertificateExpired { get; set; } = false;
            public int? CardNumber { get; set; }
            public int? NumberOfIngress { get; set; }
            public string? ExpirationDate { get; set; }
            public string? MedicalCertificateDueDate { get; set; }
            public long? SubscriptionPrice { get; set; }
            public long? InitialPrice { get; set; }

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
}