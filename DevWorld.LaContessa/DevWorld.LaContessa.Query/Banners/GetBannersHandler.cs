using DevWorld.LaContessa.Persistance;
using DevWorld.LaContessa.Query.Abstractions.Banners;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Query.Banners;

public class GetBannersHandler : IRequestHandler<GetBanners, GetBanners.Response>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public GetBannersHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task<GetBanners.Response> Handle(GetBanners request, CancellationToken cancellationToken)
    {
        return new GetBanners.Response
        {
            Banners = await _laContessaDbContext.Banners
                .AsQueryable()
                .Select(x => new GetBanners.Response.BannerDetail
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    BannerImg = x.BannerImg,
                }).ToArrayAsync(cancellationToken)
        };
    }
}
