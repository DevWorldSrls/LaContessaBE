using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;

namespace DevWorld.LaContessa.Domain.Entities.Subscriptions;

public class Subscription : SoftDeletable
{
    public User User { get; set; } = null!;
    public Activity Activity { get; set; } = null!;
    public int CardNumber { get; set; }
    public bool Valid { get; set; }
    public string ExpirationDate { get; set; } = null!;
    public string SubscriptionType { get; set; } = null!;
}
