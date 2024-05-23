using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Subscriptions;

public class DeleteSubscription : IRequest
{
    public DeleteSubscription(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}