using ToDoProject.Model.Dto.ToDo.Request;
using ToDoProject.Model.Dto.ToDo.Response;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Converter;

public class ToDoConverter
{
    public static ToDo ToEntity(AddToDoRequest request)
    {
        return new ToDo()
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Priority = request.Priority,
            CategoryId = request.CategoryId,
            UserId = request.UserId
        };
    }

    public static ToDoResponse ToDto(ToDo toDo)
    {
        return new ToDoResponse()
        {
            Id = toDo.Id,
            Title = toDo.Title,
            Description = toDo.Description,
            StartDate = toDo.StartDate,
            EndDate = toDo.EndDate,
            Priority = toDo.Priority,
            Completed = toDo.Completed
        };
    }
}