using DevWorld.LaContessa.Command.Abstractions.Banners;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Command.Services;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Banners;

public class UpdateBannerHandler : IRequestHandler<UpdateBanner>
{
    private readonly LaContessaDbContext _laContessaDbContext;
    private readonly ILaContessaFirebaseStorage _laContessaFirebaseStorage;

    public UpdateBannerHandler(
        LaContessaDbContext laContessaDbContext,
        ILaContessaFirebaseStorage laContessaFirebaseStorage
    )
    {
        _laContessaDbContext = laContessaDbContext;
        _laContessaFirebaseStorage = laContessaFirebaseStorage;
    }

    public async Task Handle(UpdateBanner request, CancellationToken cancellationToken)
    {
        var bannerToUpdate = await _laContessaDbContext.Banners.FirstOrDefaultAsync(x => x.Id == request.Banner.Id && !x.IsDeleted, cancellationToken) ?? throw new BannerNotFoundException();

        string? imageUrl = null;

        if (!string.IsNullOrEmpty(request.Banner.BannerImg))
        {
            if (bannerToUpdate.BannerImg != request.Banner.BannerImg && !(string.IsNullOrEmpty(request.Banner.BannerImgExt)))
            {
                imageUrl = await _laContessaFirebaseStorage.StoreImageData(request.Banner.BannerImg, "banners", bannerToUpdate.Id + request.Banner.BannerImgExt);
            }
            else
            {
                imageUrl = request.Banner.BannerImg;
            }
        }

        bannerToUpdate.Title = request.Banner.Title;
        bannerToUpdate.Description = request.Banner.Description;
        bannerToUpdate.BannerImg = imageUrl;

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}
