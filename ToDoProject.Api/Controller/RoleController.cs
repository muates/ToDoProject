using Microsoft.AspNetCore.Mvc;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Model.Dto.Role.Request;

namespace ToDoProject.Api.Controller;

[Route("api/v1/role")]
[ApiController]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request data");
        }
        
        await roleService.AddRoleAsync(request);

        return Ok("Success");
    }
}