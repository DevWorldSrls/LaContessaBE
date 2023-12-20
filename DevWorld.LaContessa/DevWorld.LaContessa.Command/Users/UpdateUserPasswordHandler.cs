using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Abstractions.Utilities;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPassword>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public UpdateUserPasswordHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(UpdateUserPassword request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        var generatedPassword = PasswordManager.EncryptPassword(request.NewPassword);

        userToUpdate.Password = generatedPassword;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}

