using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Users;

public class CreateUser : IRequest
{
    public UserDetail User { get; set; } = null!;

    public class UserDetail
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; }= null!;
        public string CardNumber { get; set; } = null!;
        public bool IsPro { get; set; } = false; 
        public string Email { get; set; }= null!;
        public string Password { get; set; } = null!;
        public string ImageProfile { get; set; } = null!;
    }
}
