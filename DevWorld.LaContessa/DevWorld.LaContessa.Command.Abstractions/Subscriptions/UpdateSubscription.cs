using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscriptions;

public class UpdateSbscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ActivityId { get; set; } = null!;
        public int? CardNumber { get; set; }
        public bool IsValid { get; set; }
        public string ExpirationDate { get; set; } = null!;
        public SubscriptionType SubType { get; set; }
        public int? NumberOfIngress { get; set; }
        public bool MedicalCertificateExpired { get; set; }
        public string MedicalCertificateDueDate { get; set; } = null!;
    }
}
