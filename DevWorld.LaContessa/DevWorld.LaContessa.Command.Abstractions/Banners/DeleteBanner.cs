using MediatR;

namespace DevWorld.LaContessa.Command.Abstractions.Banners;

public class DeleteBanner : IRequest
{
    public DeleteBanner(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}