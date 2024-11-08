using ToDoProject.Application.Converter;
using ToDoProject.Application.Helper;
using ToDoProject.Application.Service.Abstract;
using ToDoProject.Core.Manager.Abstract;
using ToDoProject.Core.Model.Response;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.CrossCutting.Ex;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Dto.User.Request;
using ToDoProject.Model.Dto.User.Response;

namespace ToDoProject.Application.Service.Concrete;

public class AuthService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IRoleService roleService,
    IUserRoleService userRoleService,
    ITransactionManager transactionManager)
    : IAuthService
{
    public async Task<OperationResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetUserWithRoleAsync(request.Username);

        if (user is null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidCredentialException("Invalid credentials");
        }
        
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        
        var token = TokenHelper.GenerateToken(user.Id, user.Username, roles);

        var response = UserConverter.ToDto(token, 30);

        return new OperationResponse<LoginResponse>(200, response, "Login successful");
    }

    public async Task<OperationResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        return await transactionManager.ExecuteInTransaction(async () =>
        {
            var isExistUser = await userRepository.UserExistsAsync(request.Username);
            if (isExistUser)
            {
                throw new AlreadyExistException("User already exists");
            }

            var (passwordHash, passwordSalt) = PasswordHelper.CreatePasswordHash(request.Password);

            var user = UserConverter.ToEntity(request, passwordHash, passwordSalt);
            await userRepository.AddAsync(user);

            await unitOfWork.SaveChangesAsync();

            var role = await roleService.GetRoleByNameAsync("ROLE_USER");

            if (role is null)
            {
                throw new NotFoundException("Role not found");
            }

            await userRoleService.AddUserToRoleAsync(user.Id, role.Id);

            await unitOfWork.SaveChangesAsync();

            var response = UserConverter.ToDto(user.Id, user.Username, user.Email);

            return new OperationResponse<RegisterResponse>(201, response, "User registered successfully");
        });
    }
}