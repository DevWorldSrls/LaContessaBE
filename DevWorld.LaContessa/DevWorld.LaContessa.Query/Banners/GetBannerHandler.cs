using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Banners;
using DevWorld.LaContessa.Query.Abstractions.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Banners;

public class GetBannerHandler : IRequestHandler<GetBanner, GetBanner.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBannerHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBanner.Response> Handle(GetBanner request, CancellationToken cancellationToken)
    {
        return new GetBanner.Response
        {
            Banner = await _laContessaDbContext.Banners
                .Where(y => !y.IsDeleted)
                .Select(x => new GetBanner.Response.BannerDetail
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    BannerImg = x.BannerImg,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new BannerNotFoundException()
        };
    }
}