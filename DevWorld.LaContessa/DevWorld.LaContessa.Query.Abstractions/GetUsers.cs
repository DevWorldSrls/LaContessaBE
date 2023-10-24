using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions;

public class GetUsers : IRequest<GetUsers.Response>
{
    public class Response
    {
        public User[] Users { get; set; }

        public class User
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }

}