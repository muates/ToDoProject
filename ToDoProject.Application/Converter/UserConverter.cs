using ToDoProject.Model.Dto.User.Request;
using ToDoProject.Model.Dto.User.Response;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Converter;

public abstract class UserConverter
{
    public static User ToEntity(RegisterRequest request, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsEmailConfirmed = false,
            LastLoginDate = DateTime.UtcNow
        };
    }

    public static RegisterResponse ToDto(int userId, string username, string email)
    {
        return new RegisterResponse()
        {
            UserId = userId,
            Username = username,
            Email = email,
        };
    }

    public static LoginResponse ToDto(string token, double expiration)
    {
        return new LoginResponse()
        {
            Token = token,
            Expiration = DateTime.Now.AddMinutes(expiration)
        };
    }
}