using ToDoProject.Application.Converter;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Core.Model.Response;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.CrossCutting.Ex;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Dto.Category.Request;
using ToDoProject.Model.Dto.Category.Response;

namespace ToDoProject.Application.Service.Concrete;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<OperationResponse<List<CategoryResponse>>> GetAllCategoryAsync()
    {
        var result = await categoryRepository.GetAllAsync();

        var response = result.Select(CategoryConverter.ToDto).ToList();

        return response.Count == 0
            ? throw new NotFoundException("Category not found")
            : new OperationResponse<List<CategoryResponse>>(200, response,
                "Category retrieved successfully");
    }

    public async Task<OperationResponse<CategoryResponse>> GetCategoryByIdAsync(int id)
    {
        var result = await categoryRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new NotFoundException($"Category not found with this id: {id}");
        }
        
        var response = CategoryConverter.ToDto(result);
        
        return new OperationResponse<CategoryResponse>(200, response, "Category retrieved successfully");
    }

    public async Task AddCategoryAsync(AddCategoryRequest request)
    {
        var category = CategoryConverter.ToEntity(request);
        
        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var result = await categoryRepository.GetByIdAsync(id);
        
        if (result is null)
        {
            throw new NotFoundException($"Category not found with this id: {id}");
        }
        
        await categoryRepository.DeleteAsync(result);
        await unitOfWork.SaveChangesAsync();
    }
}