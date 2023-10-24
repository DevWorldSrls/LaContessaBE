using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Users;

public class CreateUser : IRequest
{
    public UserDetail User { get; set; }

    public class UserDetail
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
