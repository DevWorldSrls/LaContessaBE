using Microsoft.AspNetCore.Mvc;

namespace DevWorld.LaContessa.API.Controllers;

[ApiController]
[Route("dummy")]
public class DummyController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<string>> GetDummy(CancellationToken cancellationToken)
    {
        return "Good job dummy";
    }
}
