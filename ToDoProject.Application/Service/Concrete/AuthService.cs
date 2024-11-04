using ToDoProject.Application.Converter;
using ToDoProject.Application.Helper;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Core.Model.Response;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.DataAccess.UnitOfWork.Abstract;
using ToDoProject.Model.Dto.User.Request;
using ToDoProject.Model.Dto.User.Response;

namespace ToDoProject.Application.Service.Concrete;

public class AuthService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IRoleService roleService,
    IUserRoleService userRoleService)
    : IAuthService
{
    public async Task<OperationResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetUserByUsernameAsync(request.Username);

        if (user is null || PasswordHelper.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new OperationResponse<LoginResponse>(401, null, "Invalid credentials");
        }

        var token = TokenHelper.GenerateToken(user.Id, user.Username);

        var response = UserConverter.ToDto(token, 30);

        return new OperationResponse<LoginResponse>(200, response, "Login successful");
    }

    public async Task<OperationResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        var isExistUser = await userRepository.UserExistsAsync(request.Username);
        if (isExistUser)
        {
            return new OperationResponse<RegisterResponse>(409, null, "User already exists");
        }

        var (passwordHash, passwordSalt) = PasswordHelper.CreatePasswordHash(request.Password);

        var user = UserConverter.ToEntity(request, passwordHash, passwordSalt);
        await userRepository.AddAsync(user);

        var role = roleService.GetRoleByNameAsync("ROLE_USER");

        if (role is null)
        {
            throw new ApplicationException("Role not found");
        }
        
        await userRoleService.AddUserToRoleAsync(user.Id, role.Id);   

        await unitOfWork.SaveChangesAsync();

        var response = UserConverter.ToDto(user.Id, user.Username, user.Email);

        return new OperationResponse<RegisterResponse>(201, response, "User registered successfully");
    }
}