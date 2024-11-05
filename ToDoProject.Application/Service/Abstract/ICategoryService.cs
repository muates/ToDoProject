using ToDoProject.Core.Model.Response;
using ToDoProject.Model.Dto.Category.Request;
using ToDoProject.Model.Dto.Category.Response;

namespace ToDoProject.Application.Service.Abstract;

public interface ICategoryService
{
    Task<OperationResponse<List<CategoryResponse>>> GetAllCategoryAsync();
    Task<OperationResponse<CategoryResponse>> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(AddCategoryRequest request);
    Task DeleteCategoryAsync(int id);
}