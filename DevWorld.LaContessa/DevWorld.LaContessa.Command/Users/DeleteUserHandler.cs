using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public DeleteUserHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();

        _laContessaDbContext.Users.Remove(userToUpdate);

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}