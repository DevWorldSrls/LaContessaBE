using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateUserHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Users.AnyAsync(x => request.User.Email == x.Email);

        if (alreadyExist)
            throw new UserAlreadyExistException();

        var userToAdd = new User
        {
            Id = Guid.NewGuid(),
            Name = request.User.Name,
            Surname = request.User.Surname,
            Email = request.User.Email,
            Password = request.User.Password,
            CardNumber = request.User.CardNumber,
            ImageProfile = request.User.ImageProfile,
            IsPro = request.User.IsPro
        };

        await _laContessaDbContext.AddAsync(userToAdd);
        await _laContessaDbContext.SaveChangesAsync();
    }
}
