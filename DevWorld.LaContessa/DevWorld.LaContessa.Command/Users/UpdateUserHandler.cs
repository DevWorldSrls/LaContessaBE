using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class UpdateUserHandler : IRequestHandler<UpdateUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateUserHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        userToUpdate.Name = request.User.Name;
        userToUpdate.Email = request.User.Email;
        userToUpdate.Name = request.User.Name;
        userToUpdate.Surname = request.User.Surname;
        userToUpdate.Email = request.User.Email;
        userToUpdate.Password = request.User.Password;
        userToUpdate.CardNumber = request.User.CardNumber;
        userToUpdate.ImageProfile = request.User.ImageProfile;
        userToUpdate.IsPro = request.User.IsPro;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
