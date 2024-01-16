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

public class LoginRequestHandler : IRequestHandler<LoginRequest, GetUser.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ITokenService _tokenService;

    public LoginRequestHandler(LaContessaDbContext laContessaDbContext, ITokenService tokenService)
    {
        _laContessaDbContext = laContessaDbContext;
        _tokenService = tokenService;
    }

    public async Task<GetUser.Response> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.IsAdmin == request.IsAdmin, cancellationToken) ?? throw new UserNotFoundException();

        bool isPasswordCorrect = PasswordManager.VerifyPassword(request.Password, user.Password);
        if (!isPasswordCorrect)
            throw new WrongPasswordException();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Email)
        };

        var token = _tokenService.GenerateAccessToken(claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);

        return new GetUser.Response
        {
            User = new GetUser.Response.UserDetail
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ImageProfile = user.ImageProfile,
                IsPro = user.IsPro,
                PeriodicBookingsEnabled = user.PeriodicBookingsEnabled,
                CardNumber = user.CardNumber,
                HasCreditCardLinked = x.CustomerId != null,
            },
            Token = token,
            RefreshToken = newRefreshToken
        };
    }
}

