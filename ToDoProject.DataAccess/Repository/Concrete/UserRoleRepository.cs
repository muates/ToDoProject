using ToDoProject.DataAccess.Repository.Abstract;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class UserRoleRepository : IUserRoleRepository
{
    public Task AddUserToRoleAsync(string userId, int roleId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserFromRoleAsync(string userId, int roleId)
    {
        throw new NotImplementedException();
    }
}