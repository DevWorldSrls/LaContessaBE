using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Users;

public class GetUser : IRequest<GetUser.Response>
{
    public GetUser(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    public class Response
    {
        public UserDetail? User { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        public class UserDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string Surname { get; set; } = null!;
            public string Email { get; set; } = null!;
            public bool IsPro { get; set; } = false;
            public bool PeriodicBookingsEnabled { get; set; } = false;
            public string? ImageProfile { get; set; }
            public string? CardNumber { get; set; }
            public bool HasCreditCardLinked { get; set; } = false;
        }
    }
}
