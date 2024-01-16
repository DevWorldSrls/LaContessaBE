namespace DevWorld.LaContessa.Domain.Entities.Banners;

public class Banner : SoftDeletable
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? BannerImg { get; set; }
    public string? BannerImgExt { get; set; }
}

