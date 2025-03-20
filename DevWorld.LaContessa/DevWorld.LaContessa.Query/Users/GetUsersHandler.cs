using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Users;

public class GetUsersHandler : IRequestHandler<GetUsers, GetUsers.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetUsersHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetUsers.Response> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        return new GetUsers.Response
        {
            Users = await _laContessaDbContext.Users
            .Where(y => !y.IsDeleted)
            .Select(x => new GetUsers.Response.UserDetail
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                ImageProfile = x.ImageProfile,
                IsPro = x.IsPro,
                PeriodicBookingsEnabled = x.PeriodicBookingsEnabled,
                CardNumber = x.CardNumber,
                HasCreditCardLinked = x.CustomerId != null,
                IsDeleted = x.IsDeleted,
            }).ToArrayAsync(cancellationToken)
        };
    }
}
