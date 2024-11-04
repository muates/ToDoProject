namespace ToDoProject.CrossCutting.Ex;

public class InvalidCredentialException : Exception
{
    public InvalidCredentialException(string message) : base(message) { }

    public InvalidCredentialException(string message, Exception innerException) : base(message, innerException) { }
}