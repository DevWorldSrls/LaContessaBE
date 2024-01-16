using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Banners;

public class GetBanners : IRequest<GetBanners.Response>
{
    public class Response
    {
        public BannerDetail[] Banners { get; set; } = null!;

        public class BannerDetail
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? BannerImg { get; set; }
            public string? BannerImgExt { get; set; }
        }
    }
}