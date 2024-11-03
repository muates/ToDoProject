namespace ToDoProject.Core.Model.Response;

public class OperationResponse<TData>(int statusCode, TData? data, string message)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public TData? Data { get; set; } = data;
}