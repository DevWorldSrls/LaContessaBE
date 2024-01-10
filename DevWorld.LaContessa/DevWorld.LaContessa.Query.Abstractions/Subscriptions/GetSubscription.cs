using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Subscriptions;

public class GetSubscription : IRequest<GetSubscription.Response>
{
    public GetSubscription(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    public class Response
    {
        public SubscriptionDetail? Subscription { get; set; } = null!;

        public class SubscriptionDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; } = null!;
            public Activity Activity { get; set; } = null!;
            public SubscriptionType SubType { get; set; }
            public bool IsValid { get; set; } = true;
            public bool MedicalCertificateExpired { get; set; } = false;
            public int? CardNumber { get; set; }
            public int? NumberOfIngress { get; set; }
            public string? ExpirationDate { get; set; }
            public string? MedicalCertificateDueDate { get; set; }
            public long? SubscriptionPrice { get; set; }
        }
    }
}
