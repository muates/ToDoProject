using ToDoProject.Core.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Abstract;

public interface IToDoRepository : IGenericRepository<ToDo, Guid>
{
    
}