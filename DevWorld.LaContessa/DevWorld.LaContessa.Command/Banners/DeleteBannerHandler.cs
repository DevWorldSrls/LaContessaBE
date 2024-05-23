using DevWorld.LaContessa.Command.Abstractions.Banners;
using DevWorld.LaContessa.Command.Abstractions.Exceptions;
using DevWorld.LaContessa.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevWorld.LaContessa.Command.Banners;

public class DeleteBannerHandler : IRequestHandler<DeleteBanner>
{
    private readonly LaContessaDbContext _laContessaDbContext;

    public DeleteBannerHandler(LaContessaDbContext laContessaDbContext)
    {
        _laContessaDbContext = laContessaDbContext;
    }

    public async Task Handle(DeleteBanner request, CancellationToken cancellationToken)
    {
        var bannerToUpdate = await _laContessaDbContext.Banners.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken) ?? throw new BannerNotFoundException();

        _laContessaDbContext.Banners.Remove(bannerToUpdate);

        await _laContessaDbContext.SaveChangesAsync(cancellationToken);
    }
}