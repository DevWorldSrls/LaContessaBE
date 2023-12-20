using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.Query.Abstractions.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Users;

public class LoginRequestHandler : IRequestHandler<LoginRequest, GetUser.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public LoginRequestHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetUser.Response> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        string enteredPassword = PasswordManager.EncryptPassword(request.Password);
        var user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken) ?? throw new UserNotFoundException();

        bool isPasswordCorrect = PasswordManager.VerifyPassword(enteredPassword, user.Password);
        if (!isPasswordCorrect)
            throw new WrongPasswordException();

        return new GetUser.Response
        {
            User = new GetUser.Response.UserDetail
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                CardNumber = user.CardNumber,
                Email = user.Email,
                ImageProfile = user.ImageProfile,
                IsPro = user.IsPro,
            }
        };
    }
}

