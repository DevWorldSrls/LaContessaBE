using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
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
        return new GetUser.Response
        {
            User = await _laContessaDbContext.Users
                .Select(x => new GetUser.Response.UserDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    CardNumber = x.CardNumber,
                    Email = x.Email,
                    ImageProfile = x.ImageProfile,
                    IsPro = x.IsPro,
                    Password = x.Password
                })
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password, cancellationToken) ?? throw new UserNotFoundException()
        };
    }
}

