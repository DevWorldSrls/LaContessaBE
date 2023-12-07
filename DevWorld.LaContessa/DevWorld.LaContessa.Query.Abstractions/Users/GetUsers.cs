using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetUsers : IRequest<GetUsers.Response>
{
    public class Response
    {
        public UserDetail[] Users { get; set; }

        public class UserDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string Surname { get; set; }
            public string CardNumber { get; set; }
            public bool IsPro { get; set; } = false;
            public string Email { get; set; }
            public string ImageProfile { get; set; }
        }
    }
}