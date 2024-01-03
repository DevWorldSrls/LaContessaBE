using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using DevWorld.LaContessa.Query.Abstractions.Users;
using DevWorld.LaContessa.Query.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace DevWorld.LaContessa.Query.Users;

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, GetUser.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ITokenService _tokenService;

    public RefreshTokenRequestHandler(LaContessaDbContext laContessaDbContext, ITokenService tokenService)
    {
        _laContessaDbContext = laContessaDbContext;
        _tokenService = tokenService;
    }

    public async Task<GetUser.Response> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AuthenticationToken);
        var userEmail = principal.Identity?.Name ?? throw new UserNotFoundException(); //TODO change in PrincipalMailNotFound
        
        var user = await _laContessaDbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail && x.RefreshToken == request.RefreshToken, cancellationToken) ?? throw new UserNotFoundException();

        var token = _tokenService.GenerateAccessToken(principal.Claims);
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
                CardNumber = user.CardNumber,
                Email = user.Email,
                ImageProfile = user.ImageProfile,
                IsPro = user.IsPro,
                PeriodicBookingsEnabled = user.PeriodicBookingsEnabled,
            },
            Token = token,
            RefreshToken = newRefreshToken
        };
    }
}

