using System.Text.Json;
using fridge_api.Modules.User.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fridge_api.Modules.User.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly GetorCreateUser _getorCreateUser;

    public UserController(GetorCreateUser getorCreateUser)
    {
        _getorCreateUser = getorCreateUser;
    }

    [HttpPost("get-or-create")]
    public async Task<ActionResult<GetorCreateUserResult>> GetOrCreateUser(
        [FromBody] JsonElement user,
        CancellationToken ct)
    {
        var result = await _getorCreateUser.GetOrCreate(new GetorCreateUserCommand(user), ct);
        return Ok(result);
    }
}
