using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Users
{
    public class SocialLoginRequest : IRequest<GetUser.Response>
    {
        public string? AppleId { get; set; }
        public string? GoogleId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
