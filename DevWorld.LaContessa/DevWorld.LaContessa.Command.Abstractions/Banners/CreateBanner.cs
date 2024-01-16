using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Banners;

public class CreateBanner : IRequest
{
    public BannerDetail Banner { get; set; } = null!;

    public class BannerDetail
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? BannerImg { get; set; }
        public string? BannerImgExt { get; set; }
    }
}
