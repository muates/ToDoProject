namespace ToDoProject.CrossCutting.Ex;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string message) : base(message)
    {
    }

    public AlreadyExistException(string message, Exception innerException) : base(message, innerException)
    {
    }
}