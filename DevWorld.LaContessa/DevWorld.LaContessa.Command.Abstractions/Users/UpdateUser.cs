using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Users;

public class UpdateUser : IRequest
{
    public UserDetail User { get; set; } = null!;

    public class UserDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
