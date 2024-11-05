using ToDoProject.Core.Model.Response;
using ToDoProject.Model.Dto.ToDo.Request;
using ToDoProject.Model.Dto.ToDo.Response;

namespace ToDoProject.Application.Service.Abstract;

public interface IToDoService
{
    Task<OperationResponse<List<ToDoResponse>>> GetAllToDoAsync();
    Task<OperationResponse<ToDoResponse>> GetToDoByIdAsync(Guid id);
    Task AddToDoAsync(AddToDoRequest request);
    Task DeleteToDoAsync(Guid id);
}