﻿using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Users;

public class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public CreateUserHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Users.Where(x => request.User.Email == x.Email).AnyAsync();

        if (alreadyExist)
            throw new UserAlreadyExistException();

        var userToAdd = new User 
        { 
            Id = Guid.NewGuid(),
            Name = request.User.Name,
            Email = request.User.Email
        };

        await _laContessaDbContext.AddAsync(userToAdd);

        await _laContessaDbContext.SaveChangesAsync();
    }
}
