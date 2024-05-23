using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Users;

public class DeleteUser : IRequest
{
    public DeleteUser(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}