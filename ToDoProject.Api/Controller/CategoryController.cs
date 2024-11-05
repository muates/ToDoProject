using Microsoft.AspNetCore.Mvc;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Model.Dto.Category.Request;

namespace ToDoProject.Api.Controller;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
    {
        await categoryService.AddCategoryAsync(request);
        
        return Ok("Category added successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await categoryService.GetAllCategoryAsync();
        
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var result = await categoryService.GetCategoryByIdAsync(id);
        
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await categoryService.DeleteCategoryAsync(id);
        
        return Ok("Category deleted successfully");
    }
}