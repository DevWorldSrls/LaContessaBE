using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Users;

public class UpdateUserPassword : IRequest
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}