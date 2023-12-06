using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query;

public class GetUserHandler : IRequestHandler<GetUser, GetUser.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetUserHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetUser.Response> Handle(GetUser request, CancellationToken cancellationToken)
    {
        return new GetUser.Response
        {
            User = await _laContessaDbContext.Users
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(),
        };
    }
}
