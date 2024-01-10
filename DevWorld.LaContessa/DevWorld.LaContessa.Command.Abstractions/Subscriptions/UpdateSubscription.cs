using DevWorld.LaContessa.Domain.Enums;
using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscriptions;

public class UpdateSbscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
        public SubscriptionType SubType { get; set; }
        public bool IsValid { get; set; } = true;
        public bool MedicalCertificateExpired { get; set; } = false;
        public int? CardNumber { get; set; }
        public int? NumberOfIngress { get; set; }
        public string? ExpirationDate { get; set; }
        public string? MedicalCertificateDueDate { get; set; }
        public long? SubscriptionPrice { get; set; }
        public long? PaymentPrice { get; set; }
        public bool IsPaymentRequest { get; set; } = false;
    }
}
