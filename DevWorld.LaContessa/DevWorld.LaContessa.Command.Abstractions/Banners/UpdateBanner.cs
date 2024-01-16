using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Banners;

public class UpdateBanner : IRequest
{
    public BannerDetail Banner { get; set; } = null!;

    public class BannerDetail
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? BannerImg { get; set; }
        public string? BannerImgExt { get; set; }
    }
}
