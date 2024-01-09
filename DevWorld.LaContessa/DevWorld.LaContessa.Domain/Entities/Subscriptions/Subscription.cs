using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;

namespace DevWorld.LaContessa.Domain.Entities.Subscriptions;

public class Subscription : SoftDeletable
{
    public User User { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public SubscriptionType SubType { get; set; }
    public bool IsValid { get; set; } = true;
    public bool MedicalCertificateExpired { get; set; } = false;
    public int? CardNumber { get; set; }
    public int? NumberOfIngress { get; set; }
    public string? ExpirationDate { get; set; }
    public string? MedicalCertificateDueDate { get; set; }
}
