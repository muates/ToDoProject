using Microsoft.AspNetCore.Mvc;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Model.Dto.User.Request;

namespace ToDoProject.Api.Controller;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request data");
        }

        var result = await authService.LoginAsync(request);

        if (result.StatusCode == 401)
        {
            return Unauthorized(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request data");
        }

        var result = await authService.RegisterAsync(request);

        if (result.StatusCode == 409)
        {
            return Conflict(result.Message);
        }

        return CreatedAtAction(nameof(Register), result);
    }
}