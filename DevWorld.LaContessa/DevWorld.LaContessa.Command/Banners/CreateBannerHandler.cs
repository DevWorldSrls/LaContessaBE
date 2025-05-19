using DevWorld.LaContessa.Command.Abstractions.Banners;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Banners;

public class CreateBannerHandler : IRequestHandler<CreateBanner>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public CreateBannerHandler(LaContessaDbContext laContessaDbContext, ILaContessaFirebaseStorage laContessaFirebaseStorage)
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(CreateBanner request, CancellationToken cancellationToken)
    {
        var alreadyExist = await _laContessaDbContext.Banners.AnyAsync(x => 
            request.Banner.Title == x.Title && !x.IsDeleted,
            cancellationToken
        );

        if (alreadyExist)
            throw new BannerAlreadyExistException();

        string? imageUrl = null;
        var newBunnerId = Guid.NewGuid();

        if (!(string.IsNullOrEmpty(request.Banner.BannerImg) || string.IsNullOrEmpty(request.Banner.BannerImgExt)))
        {
            imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.Banner.BannerImg, "banners", newBunnerId + request.Banner.BannerImgExt);
        }

        var bannerToAdd = new Domain.Entities.Banners.Banner
        {
            Id = newBunnerId,
            Title = request.Banner.Title,
            Description = request.Banner.Description,
            BannerImg = imageUrl,
        };

        await _laContessaDbContext.AddAsync(bannerToAdd, cancellationToken);
        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
