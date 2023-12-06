using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscription;

public class UpdateSbscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int CardNumber { get; set; }
        public bool Valid { get; set; }
        public string ExpirationDate { get; set; }
        public string SubscriptionType { get; set; }
    }
}
