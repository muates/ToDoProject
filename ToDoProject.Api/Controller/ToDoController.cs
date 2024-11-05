using Microsoft.AspNetCore.Mvc;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Model.Dto.ToDo.Request;

namespace ToDoProject.Api.Controller;

[ApiController]
[Route("api/v1/to-do")]
public class ToDoController(IToDoService toDoService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddToDo([FromBody] AddToDoRequest request)
    {
        await toDoService.AddToDoAsync(request);

        return Ok("ToDo added successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllToDos()
    {
        var result = await toDoService.GetAllToDoAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetToDoById([FromRoute] Guid id)
    {
        var result = await toDoService.GetToDoByIdAsync(id);
        
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteToDo([FromRoute] Guid id)
    {
        await toDoService.DeleteToDoAsync(id);

        return Ok("ToDo deleted successfully");
    }
}