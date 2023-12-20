using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscriptions;

public class CreateSubscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public string UserId { get; set; } = null!;
        public string ActivityId { get; set; } = null!;
        public int CardNumber { get; set; }
        public bool Valid { get; set; }
        public string ExpirationDate { get; set; } = null!;
        public string SubscriptionType { get; set; } = null!;
    }
}
