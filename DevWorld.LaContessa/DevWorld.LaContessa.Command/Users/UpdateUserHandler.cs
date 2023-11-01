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
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id && !x.IsDeleted);

        if (userToUpdate is null)
            throw new UserNotFoundException();

        userToUpdate.Name = request.User.Name;
        userToUpdate.Email = request.User.Email;

        await _laContessaDbContext.SaveChangesAsync();
    }
}
