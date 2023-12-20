using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Query.Abstractions.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetUsers.Response>> GetUsers(CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetUsers(),
            cancellationToken
        );
    }

    [HttpGet("id")]
    public async Task<ActionResult<GetUser.Response>> GetUser(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new GetUser(id),
            cancellationToken
        );
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUser.UserDetail user,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CreateUser
            {
                User = user
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUser.UserDetail user,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateUser
            {
                User = user
            },
            cancellationToken
        );

        return Ok();
    }

    [HttpGet("login")]
    public async Task<ActionResult<GetUser.Response>> Login(string email, string password, CancellationToken cancellationToken)
    {
        return await _mediator.Send(
            new LoginRequest
            {
                Email = email,
                Password = password
            },
            cancellationToken
        );
    }
}
