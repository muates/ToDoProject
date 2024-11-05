using Microsoft.AspNetCore.Mvc;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.CrossCutting.Validation.Abstract;
using ToDoProject.Model.Dto.User.Request;

namespace ToDoProject.Api.Controller;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService, IValidationStrategy<RegisterRequest> registerRequestValidation)
    : ControllerBase
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
        registerRequestValidation.Validate(request);

        var result = await authService.RegisterAsync(request);
        
        return CreatedAtAction(nameof(Register), result);
    }
}