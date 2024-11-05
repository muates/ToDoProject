using ToDoProject.Model.Enum;

namespace ToDoProject.Model.Dto.ToDo.Request;

public class AddToDoRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}