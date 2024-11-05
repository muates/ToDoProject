using ToDoProject.Application.Converter;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Core.Model.Response;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.CrossCutting.Ex;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Dto.ToDo.Request;
using ToDoProject.Model.Dto.ToDo.Response;

namespace ToDoProject.Application.Service.Concrete;

public class ToDoService(IToDoRepository toDoRepository, IUnitOfWork unitOfWork) : IToDoService
{
    public async Task<OperationResponse<List<ToDoResponse>>> GetAllToDoAsync()
    {
        var result = await toDoRepository.GetAllAsync();

        var response = result.Select(ToDoConverter.ToDto).ToList();

        return response.Count == 0
            ? throw new NotFoundException("ToDo not found")
            : new OperationResponse<List<ToDoResponse>>(200, response, "ToDo retrieved successfully");
    }

    public async Task<OperationResponse<ToDoResponse>> GetToDoByIdAsync(Guid id)
    {
        var result = await toDoRepository.GetByIdAsync(id);

        if (result is null)
        {
            throw new NotFoundException($"ToDo not found with this id: {id}");
        }

        var response = ToDoConverter.ToDto(result);

        return new OperationResponse<ToDoResponse>(200, response, "ToDo retrieved successfully");
    }

    public async Task AddToDoAsync(AddToDoRequest request)
    {
        var toDo = ToDoConverter.ToEntity(request);

        await toDoRepository.AddAsync(toDo);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteToDoAsync(Guid id)
    {
        var result = await toDoRepository.GetByIdAsync(id);
        
        if (result is null)
        {
            throw new NotFoundException($"ToDo not found with this id: {id}");
        }

        await toDoRepository.DeleteAsync(result);
        await unitOfWork.SaveChangesAsync();
    }
}