namespace ToDoProject.Model.Dto.User.Response;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}