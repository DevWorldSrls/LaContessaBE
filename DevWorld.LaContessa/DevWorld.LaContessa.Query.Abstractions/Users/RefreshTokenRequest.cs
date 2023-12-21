using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Users
{
    public class RefreshTokenRequest : IRequest<GetUser.Response>
    {
        public string AuthenticationToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
