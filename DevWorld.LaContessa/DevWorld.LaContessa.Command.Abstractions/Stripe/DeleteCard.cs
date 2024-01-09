using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Stripe
{
    public class DeleteCard : IRequest
    {
        public Guid UserId { get; set; }
    }
}
