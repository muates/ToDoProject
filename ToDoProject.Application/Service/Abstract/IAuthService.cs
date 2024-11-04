using ToDoProject.Core.Model.Response;
using ToDoProject.Model.Dto.User.Request;
using ToDoProject.Model.Dto.User.Response;

namespace ToDoProject.Application.Service.Abstract;

public interface IAuthService
{
    Task<OperationResponse<LoginResponse>> LoginAsync(LoginRequest request);
    Task<OperationResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);
}