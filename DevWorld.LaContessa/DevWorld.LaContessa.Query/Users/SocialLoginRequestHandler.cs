using DevWorld.LaContessa.Domain.Entities.Users;
using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.Query.Abstractions.Utilities;
using DevWorld.LaContessa.Query.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevWorld.LaContessa.Query.Users;

public class SocialLoginRequestHandler : IRequestHandler<SocialLoginRequest, GetUser.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ITokenService _tokenService;

    public SocialLoginRequestHandler(
        LaContessaDbContext laContessaDbContext, 
        ITokenService tokenService
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _tokenService = tokenService;
    }

    public async Task<GetUser.Response> Handle(SocialLoginRequest request, CancellationToken cancellationToken)
    {
        User? user = null;
        (string token, string refresh) newTokens;

        if (request.AppleId is not null)
            user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.AppleUserId == request.AppleId, cancellationToken);

        if (request.GoogleId is not null)
            user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.GoogleUserId == request.GoogleId, cancellationToken);

        if (user is null) 
        {
            var emailAlreadyExist = await _laContessaDbContext.Users.AnyAsync(x => x.Email == request.Email);
            if (emailAlreadyExist) throw new UserEmailAlreadyExistException();

            var generatedPassword = PasswordManager.EncryptPassword(request.AppleId ?? request.GoogleId ?? "");

            user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name ?? "",
                Surname = "",
                Email = request.Email ?? "",
                Password = generatedPassword,
                IsPro = false,
                PeriodicBookingsEnabled = false,
                GoogleUserId = request.GoogleId,
                AppleUserId = request.AppleId
            };

            newTokens = GenerateSocialToken(request.Email ?? "");
            user.RefreshToken = newTokens.refresh;

            await _laContessaDbContext.AddAsync(user);
        }
        else
        {
            newTokens = GenerateSocialToken(user.Email ?? "");
            user.RefreshToken = newTokens.refresh;
        }

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);

        return new GetUser.Response
        {
            User = new GetUser.Response.UserDetail
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email!,
                ImageProfile = user.ImageProfile,
                IsPro = user.IsPro,
                PeriodicBookingsEnabled = user.PeriodicBookingsEnabled,
                CardNumber = user.CardNumber,
                HasCreditCardLinked = user.CustomerId != null,
            },
            Token = newTokens.token,
            RefreshToken = newTokens.refresh
        };
    }

    private (string token, string refresh) GenerateSocialToken(string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Name, email)
        };

        var tk = _tokenService.GenerateAccessToken(claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        return (token: tk, refresh: newRefreshToken);
    }
}

