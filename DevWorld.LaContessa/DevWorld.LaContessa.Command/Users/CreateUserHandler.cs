using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Command.Abstractions.Utilities;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public CreateUserHandler(LaContessaDbContext laContessaDbContext, ILaContessaFirebaseStorage laContessaFirebaseStorage)
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Users.AnyAsync(x => request.User.Email == x.Email);

        if (alreadyExist)
            throw new UserAlreadyExistException();

        string? imageUrl = null;
        var newUserId = Guid.NewGuid();

        if (!(string.IsNullOrEmpty(request.User.ImageProfile) || string.IsNullOrEmpty(request.User.ImageProfileExt)))
        {
            imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.User.ImageProfile, "users", newUserId + request.User.ImageProfileExt);
        }

        var generatedPassword = PasswordManager.EncryptPassword(request.User.Password);

        var userToAdd = new User
        {
            Id = newUserId,
            Name = request.User.Name,
            Surname = request.User.Surname,
            Email = request.User.Email,
            Password = generatedPassword,
            ImageProfile = imageUrl,
            IsPro = request.User.IsPro,
            PeriodicBookingsEnabled = request.User.PeriodicBookingsEnabled,
            CardNumber = request.User.CardNumber,
        };

        await _laContessaDbContext.AddAsync(userToAdd);
        await _laContessaDbContext.SaveChangesAsync();
    }
}
