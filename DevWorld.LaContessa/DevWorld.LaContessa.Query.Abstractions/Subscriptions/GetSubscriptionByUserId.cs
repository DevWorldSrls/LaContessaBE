using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Users;
using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Subscriptions;

public class GetSubscriptionByUserId : IRequest<GetSubscriptionByUserId.Response>
{
    public GetSubscriptionByUserId(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; } = null!;

    public class Response
    {
        public SubscriptionDetail? Subscription { get; set; } = null!;

        public class SubscriptionDetail
        {
            public Guid Id { get; set; }
            public User User { get; set; } = null!;
            public Activity Activity { get; set; } = null!;
            public int CardNumber { get; set; }
            public bool Valid { get; set; }
            public string ExpirationDate { get; set; } = null!;
            public string SubscriptionType { get; set; } = null!;
        }
    }
}