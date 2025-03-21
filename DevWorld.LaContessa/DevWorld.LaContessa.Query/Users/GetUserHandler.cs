﻿using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Users;

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
                .Where(y => !y.IsDeleted)
                .Select(x => new GetUser.Response.UserDetail
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
                    IsSocialLogged = x.GoogleUserId != null || x.AppleUserId != null,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new UserNotFoundException()
        };
    }
}
