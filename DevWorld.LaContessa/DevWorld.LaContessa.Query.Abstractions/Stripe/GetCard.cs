using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Stripe;

public class GetCard : IRequest<GetCard.Response>
{
    public GetCard(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
    public class Response
    {
        public CardDetail Card { get; set; } = null!;

        public class CardDetail
        {
            public string LastFour { get; set; } = null!;
            public long ExpirationYear { get; set; }
            public long ExpirationMonth { get; set; }        }
    }
}