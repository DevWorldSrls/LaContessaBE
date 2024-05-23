using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Activites;

public class DeleteActivity : IRequest
{
    public DeleteActivity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
