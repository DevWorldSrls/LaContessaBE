using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Domain.Enums;

namespace DevWorld.LaContessa.Domain.Entities.Subscriptions;

public class Subscription : SoftDeletable
{
    public User User { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public int? CardNumber { get; set; }
    public bool IsValid { get; set; }
    public string ExpirationDate { get; set; } = null!;
    public SubscriptionType SubType { get; set; }
    public int? NumberOfIngress { get; set; }
    public bool MedicalCertificateExpired { get; set; }
    public string MedicalCertificateDueDate { get; set; } = null!;
}
