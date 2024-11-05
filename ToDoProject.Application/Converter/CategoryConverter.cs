using ToDoProject.Model.Dto.Category.Request;
using ToDoProject.Model.Dto.Category.Response;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Converter;

public class CategoryConverter
{
    public static Category ToEntity(AddCategoryRequest request)
    {
        return new Category()
        {
            Name = request.Name
        };
    }

    public static CategoryResponse ToDto(Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id, 
            Name = category.Name
        };
    }
}