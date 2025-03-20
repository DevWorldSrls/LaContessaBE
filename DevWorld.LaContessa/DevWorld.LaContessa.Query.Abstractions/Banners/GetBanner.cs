using MediatR;

namespace DevWorld.LaContessa.Query.Abstractions.Banners;

public class GetBanner : IRequest<GetBanner.Response>
{
    public GetBanner(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

    public class Response
    {
        public BannerDetail? Banner { get; set; } = null!;

        public class BannerDetail
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public string? BannerImg { get; set; }
        }
    }
}
