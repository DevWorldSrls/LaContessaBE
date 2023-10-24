using DevWorld.LaContessa.Command.Abstractions.Users;
using DevWorld.LaContessa.Query.Abstractions;
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

	[HttpPost]
	public async Task<ActionResult> CreateUser([FromBody] CreateUser.UserDetail user, CancellationToken cancellationToken)
	{
		await _mediator.Send(
			new CreateUser
			{
				User = user,
			},
			cancellationToken
		);

		return Ok();
	}
}
