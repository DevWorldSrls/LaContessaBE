using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Users;

public class GetUsers : IRequest<GetUsers.Response>
{
    public class Response
    {
        public UserDetail[] Users { get; set; } = null!;

        public class UserDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string Surname { get; set; } = null!;
            public string CardNumber { get; set; } = null!;
            public bool IsPro { get; set; } = false;
            public string Email { get; set; } = null!;
            public string ImageProfile { get; set; } = null!;
        }
    }
}