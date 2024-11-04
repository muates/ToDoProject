using ToDoProject.Application.Service.Abstract;
using ToDoProject.CrossCutting.Ex;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.DataAccess.UnitOfWork.Abstract;
using ToDoProject.Model.Dto.Role.Request;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Service.Concrete;

public class RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRoleService
{
    public async Task<IReadOnlyCollection<Role>> GetAllRoleAsync()
    {
        return await roleRepository.GetAllAsync();
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            throw new NotFoundException("Role not found");
        }

        return role;
    }

    public async Task<Role> GetRoleByNameAsync(string name)
    {
        var role = await roleRepository.GetRoleByNameAsync(name);

        if (role is null)
        {
            throw new NotFoundException("Role not found");
        }

        return role;
    }

    public async Task AddRoleAsync(AddRoleRequest request)
    {
        var role = new Role() { Name = request.Name };

        await roleRepository.AddAsync(role);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int id)
    {
        var existRole = await GetRoleByIdAsync(id);

        await roleRepository.DeleteAsync(existRole);
        await unitOfWork.SaveChangesAsync();
    }
}