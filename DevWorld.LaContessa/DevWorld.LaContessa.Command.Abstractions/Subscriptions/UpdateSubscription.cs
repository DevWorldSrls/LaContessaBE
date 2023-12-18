using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscription;

public class UpdateSbscription : IRequest
{
    public SubscriptionDetail Subscription { get; set; } = null!;

    public class SubscriptionDetail
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public int CardNumber { get; set; }
        public bool Valid { get; set; }
        public string ExpirationDate { get; set; } = null!;
        public string SubscriptionType { get; set; } = null!;
    }
}
