using ToDoProject.Application.Service.Abstract;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Service.Concrete;

public class UserRoleService(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
    : IUserRoleService
{
    public async Task AddUserToRoleAsync(int userId, int roleId)
    {
        var userRole = new UserRole() { UserId = userId, RoleId = roleId };

        await userRoleRepository.AddUserToRoleAsync(userRole);
        await unitOfWork.SaveChangesAsync();
    }
}