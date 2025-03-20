using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class UpdateUserHandler : IRequestHandler<UpdateUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public UpdateUserHandler(LaContessaDbContext laContessaDbContext, ILaContessaFirebaseStorage laContessaFirebaseStorage)
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id && !x.IsDeleted, cancellationToken) ?? throw new UserNotFoundException();
        
        string? imageUrl = null;

        if (!string.IsNullOrEmpty(request.User.ImageProfile))
        {
            if (userToUpdate.ImageProfile != request.User.ImageProfile && !(string.IsNullOrEmpty(request.User.ImageProfileExt)))
            {
                imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.User.ImageProfile, "users", userToUpdate.Id + request.User.ImageProfileExt);
            }
            else
            {
                imageUrl = request.User.ImageProfile;
            }
        }

        userToUpdate.Name = request.User.Name;
        userToUpdate.Email = request.User.Email;
        userToUpdate.Name = request.User.Name;
        userToUpdate.Surname = request.User.Surname;
        userToUpdate.Email = request.User.Email;
        userToUpdate.ImageProfile = imageUrl;
        userToUpdate.IsPro = request.User.IsPro;
        userToUpdate.PeriodicBookingsEnabled = request.User.PeriodicBookingsEnabled;
        userToUpdate.CardNumber = request.User.CardNumber;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
