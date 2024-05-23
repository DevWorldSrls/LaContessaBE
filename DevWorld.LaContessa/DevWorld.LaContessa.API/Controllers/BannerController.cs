using DevWorld.LaContessa.Command.Abstractions.Banners;
using DevWorld.LaContessa.Query.Abstractions.Banners;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Authorize]
[Route("banner")]
public class BannerController : ControllerBase
{
    private readonly IMediator _mediator;

    public BannerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetBanners.Response>> GetBanners(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetBanners(),
            cancellationToken
        );
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetBanner.Response>> GetBanner(Guid id,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetBanner(id),
            cancellationToken
        );
    }

    [HttpPost]
    public async Task<ActionResult> CreateBanner([FromBody] CreateBanner.BannerDetail banner,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateBanner
            {
                Banner = banner
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBanner([FromBody] UpdateBanner.BannerDetail banner,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateBanner
            {
                Banner = banner
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteBanner(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteBanner(id),
            cancellationToken
        );

        return Ok();
    }
}
