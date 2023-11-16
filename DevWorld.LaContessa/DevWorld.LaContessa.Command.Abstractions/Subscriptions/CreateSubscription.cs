using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscription;

public class CreateSubscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public string UserId { get; set; } = null!;
        public int Number { get; set; } 
        public bool Valid { get; set; }
    }
}
