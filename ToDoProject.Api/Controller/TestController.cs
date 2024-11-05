using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoProject.Api.Controller;

[ApiController]
[Route("api/v1/test")]
public class TestController : ControllerBase
{
    [Authorize(Roles = "ROLE_USER")]
    [HttpGet("user")]
    public IActionResult GetUser()
    {
        return Ok("Success");
    }
    
    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpGet("admin")]
    public IActionResult GetAdmin()
    {
        return Ok("Success");
    }
}