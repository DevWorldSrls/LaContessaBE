using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Stripe
{
    public class RefundRequest : IRequest
    {
        public Guid BookingId { get; set; }
    }
}
