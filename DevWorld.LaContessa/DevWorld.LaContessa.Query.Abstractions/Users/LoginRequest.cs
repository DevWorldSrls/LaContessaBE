using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Users
{
    public class LoginRequest : IRequest<GetUser.Response>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
