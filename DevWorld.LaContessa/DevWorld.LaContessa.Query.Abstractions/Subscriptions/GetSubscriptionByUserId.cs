using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetSubscriptionByUserId : IRequest<GetSubscriptionByUserId.Response>
{
    public string UserId { get; set; }

    public GetSubscriptionByUserId(string userId)
    {
        UserId = userId;
    }

    public class Response
    {
        public SubscriptionDetail? Subscription { get; set; }

        public class SubscriptionDetail
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public int Number { get; set; }
            public bool Valid { get; set; }
        }
    }

}